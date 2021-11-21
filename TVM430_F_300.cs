using Orts.Simulation.Signalling;
using ORTS.Scripting.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using static ORTS.Scripting.Script.TVM430Common;

namespace ORTS.Scripting.Script
{
    public class TVM430_F_300 : CsSignalScript
    {
        Dictionary<TVMSpeedType, TVMSpeedType> TAB1 = SNCFV320TAB1;
        Dictionary<TVMSpeedType, TVMSpeedType> TAB2 = SNCFV320TAB2;

        TVMSpeedType[] Vpf = new TVMSpeedType[2] { TVMSpeedType._300V, TVMSpeedType._300V };
        TVMSpeedType Vcond = TVMSpeedType._300V;
        bool RRRAval = false;

        Timer AspectChangeTimer;
        TVMSpeedType VeE = TVMSpeedType._000;
        TVMSpeedType VcE = TVMSpeedType._RRR;
        TVMSpeedType VaE = TVMSpeedType.Any;

        public override void Initialize()
        {
            if (IsSignalFeatureEnabled("USER4"))
            {
                Vpf[1] = TVMSpeedType._80E;
            }
            else if (IsSignalFeatureEnabled("USER3"))
            {
                Vpf[1] = TVMSpeedType._170E;
            }
            else if (IsSignalFeatureEnabled("USER2"))
            {
                Vpf[1] = TVMSpeedType._230V;
            }
            else if (IsSignalFeatureEnabled("USER1"))
            {
                Vpf[1] = TVMSpeedType._270V;
            }
            else
            {
                Vpf[1] = TVMSpeedType._300V;
            }

            AspectChangeTimer = new Timer(this);
            AspectChangeTimer.Setup(6f);
        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            List<string> nextNormalParts = new List<string>();
            if (nextNormalSignalId >= 0)
            {
                nextNormalParts = IdTextSignalAspect(nextNormalSignalId, "NORMAL").Split(' ').ToList();
                SendSignalMessage(nextNormalSignalId, "FR_TVM430 Vpf" + Vpf[1].ToString().Substring(1));
            }

            TVMSpeedType[] Ve = new TVMSpeedType[2] { TVMSpeedType.Any, TVMSpeedType.Any };
            TVMSpeedType[] Vc = new TVMSpeedType[2] { TVMSpeedType.Any, TVMSpeedType.Any };
            TVMSpeedType[] Va = new TVMSpeedType[2] { TVMSpeedType.Any, TVMSpeedType.Any };

            foreach (string part in nextNormalParts)
            {
                if (part.StartsWith("Ve"))
                {
                    Ve[1] = (TVMSpeedType)Enum.Parse(typeof(TVMSpeedType), "_" + part.Substring(2));
                }
                else if (part.StartsWith("Vc"))
                {
                    Vc[1] = (TVMSpeedType)Enum.Parse(typeof(TVMSpeedType), "_" + part.Substring(2));
                }
                else if (part.StartsWith("Va"))
                {
                    Va[1] = (TVMSpeedType)Enum.Parse(typeof(TVMSpeedType), "_" + part.Substring(2));
                }
            }

            if (CurrentBlockState != BlockState.Clear
                || !nextNormalParts.Contains("FR_TVM430")
                || Ve[1] == TVMSpeedType.Any
                || Vc[1] == TVMSpeedType.Any)
            {
                Vcond = TVMSpeedType._RRR;
                Ve[1] = TVMSpeedType._000;
                Vc[1] = TVMSpeedType._RRR;
                RRRAval = true;
            }
            else
            {
                Vcond = Vpf[0];
                Ve[1] = Min(Ve[1], TAB2[Vpf[1]]);
                RRRAval = false;
            }

            Vc[0] = Min(Vcond, Ve[1]);
            Ve[0] = Min(TAB2[Vcond], TAB1[Vc[0]]);
            Va[0] = TAB2[Vc[1]];

            if (Va[0] >= Vc[0])
            {
                Va[0] = TVMSpeedType.Any;
            }

            if (Ve[0] != VeE || Vc[0] != VcE || Va[0] != VaE)
            {
                if (Ve[0] < VeE || Vc[0] < VcE || VcE == TVMSpeedType._RRR)
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
            TextSignalAspect = "FR_TVM430"
                + " Ve" + VeE.ToString().Substring(1)
                + " Vc" + VcE.ToString().Substring(1)
                + (VaE != TVMSpeedType.Any ? " Va" + VaE.ToString().Substring(1) : string.Empty)
                + (RRRAval ? " RRRAval" : string.Empty);

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
                        Vpf[0] = (TVMSpeedType)Enum.Parse(typeof(TVMSpeedType), "_" + part.Substring(3));
                    }
                }
            }
        }
    }
}