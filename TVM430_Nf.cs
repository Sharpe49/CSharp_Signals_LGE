using ORTS.Scripting.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using static ORTS.Scripting.Script.TVM430Common;

namespace ORTS.Scripting.Script
{
    public class TVM430_Nf : FrSignalScript
    {
        Dictionary<TvmSpeedType, TvmSpeedType> TAB1 = SNCFV320TAB1;
        Dictionary<TvmSpeedType, TvmSpeedType> TAB2 = SNCFV320TAB2;

        TvmSpeedType[] Vpf = new TvmSpeedType[2] { TvmSpeedType._320V, TvmSpeedType._320V };
        TvmSpeedType Vcond = TvmSpeedType._320V;
        bool CNf = false;

        Timer AspectChangeTimer;

        public override void Initialize()
        {
            if (IsSignalFeatureEnabled("USER4"))
            {
                Vpf[1] = TvmSpeedType._80E;
            }
            else if (IsSignalFeatureEnabled("USER3"))
            {
                Vpf[1] = TvmSpeedType._170E;
            }
            else if (IsSignalFeatureEnabled("USER2"))
            {
                Vpf[1] = TvmSpeedType._270V;
            }
            else if (IsSignalFeatureEnabled("USER1"))
            {
                Vpf[1] = TvmSpeedType._300V;
            }
            else
            {
                Vpf[1] = TvmSpeedType._320V;
            }

            AspectChangeTimer = new Timer(this);
            AspectChangeTimer.Setup(6f);

            TvmType = TvmType.FR_TVM430;
            VeE = TvmSpeedType._000;
            VcE = TvmSpeedType._RRR;
            VaE = TvmSpeedType.Any;
        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            if (nextNormalSignalId >= 0)
            {
                SendSignalMessage(nextNormalSignalId, "FR_TVM430 Vpf" + Vpf[1].ToString().Substring(1));
            }

            SignalInfo nextNormalSignalInfo = DeserializeAspect(nextNormalSignalId, "NORMAL");

            TvmSpeedType[] Ve = new TvmSpeedType[2] { TvmSpeedType.Any, nextNormalSignalInfo.Ve };
            TvmSpeedType[] Vc = new TvmSpeedType[2] { TvmSpeedType.Any, nextNormalSignalInfo.Vc };
            TvmSpeedType[] Va = new TvmSpeedType[2] { TvmSpeedType.Any, nextNormalSignalInfo.Va };

            // Repère Nf fermé => Arret réduit + BSP CNf puis marche à vue (RRR)
            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || Ve[1] == TvmSpeedType.None
                || Vc[1] == TvmSpeedType.None
                || Ve[1] == TvmSpeedType.Any
                || Vc[1] == TvmSpeedType.Any)
            {
                Vcond = TvmSpeedType._80E;
                Ve[1] = TvmSpeedType._000;
                Vc[1] = TvmSpeedType._RRR;
                CNf = true;
            }
            // Entrée sur VS => Arrêt réduit puis marche à vue (RRR)
            else if (nextNormalSignalInfo.TvmType != TvmType.FR_TVM430)
            {
                Vcond = TvmSpeedType._80E;
                Ve[1] = TvmSpeedType._000;
                Vc[1] = TvmSpeedType._RRR;
                CNf = false;
            }
            else
            {
                Vcond = Vpf[0];
                Ve[1] = Min(Ve[1], TAB2[Vpf[1]]);
                CNf = false;
            }

            Vc[0] = Min(Vcond, Ve[1]);
            Ve[0] = Min(TAB2[Vcond], TAB1[Vc[0]]);
            Va[0] = TAB2[Vc[1]];

            if (Va[0] >= Vc[0])
            {
                Va[0] = TvmSpeedType.Any;
            }

            if (Ve[0] != VeE || Vc[0] != VcE || Va[0] != VaE)
            {
                if (Ve[0] < VeE || Vc[0] < VcE || VcE == TvmSpeedType._RRR)
                {
                    VeE = Ve[0];
                    VcE = Vc[0];
                    VaE = Va[0];
                    AspectChangeTimer.Start();
                }
                else
                {
                    if (!PreUpdate() && AspectChangeTimer.Started)
                    {
                        if (AspectChangeTimer.Triggered)
                        {
                            AspectChangeTimer.Stop();
                        }
                    }
                    else
                    {
                        VeE = Ve[0];
                        VcE = Vc[0];
                        VaE = Va[0];
                    }
                }
            }

            Tvm430BspMessage = CNf ? Tvm430BspMessage.BSP_CNf : Tvm430BspMessage.None;

            MstsSignalAspect = TVMSpeedTypeToAspectV320(VcE, !CNf);
            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }

        public override void HandleSignalMessage(int signalId, string message)
        {
            List<string> parts = message.Split(' ').ToList();
            if (parts.Contains("FR_TVM430"))
            {
                foreach (string part in parts)
                {
                    if (part.StartsWith("Vpf"))
                    {
                        Vpf[0] = (TvmSpeedType)Enum.Parse(typeof(TvmSpeedType), "_" + part.Substring(3));
                    }
                }
            }
        }
    }
}