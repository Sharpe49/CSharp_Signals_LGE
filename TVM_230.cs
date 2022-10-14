using ORTS.Scripting.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using static ORTS.Scripting.Script.TVM430Common;

namespace ORTS.Scripting.Script
{
    public class TVM_230 : FrSignalScript
    {
        Dictionary<TvmSpeedType, TvmSpeedType> TAB2 = SNCFV320TAB2;

        TvmSpeedType[] Vpf = new TvmSpeedType[2] { TvmSpeedType._320V, TvmSpeedType._230E };
        TvmSpeedType Vcond = TvmSpeedType._320V;

        Timer AspectChangeTimer;

        public override void Initialize()
        {
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

            if (CurrentBlockState != BlockState.Clear
                || nextNormalSignalInfo.TvmType != TvmType.FR_TVM430
                || Ve[1] == TvmSpeedType.None
                || Vc[1] == TvmSpeedType.None
                || Ve[1] == TvmSpeedType.Any
                || Vc[1] == TvmSpeedType.Any)
            {
                Vcond = TvmSpeedType._RRR;
            }
            else
            {
                Vcond = Vpf[0];
                Ve[1] = Min(Ve[1], TAB2[Vpf[1]]);
            }

            Vc[0] = Min(Vcond, Vc[1]);
            Ve[0] = Min(TAB2[Vcond], Ve[1]);
            Va[0] = Va[1];

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

            MstsSignalAspect = TVMSpeedTypeToAspectV320(VcE, true);
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