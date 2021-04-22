using Orts.Simulation.Signalling;
using System;
using System.Collections.Generic;
using System.Linq;
using static ORTS.Scripting.Script.TVM430Common;

namespace ORTS.Scripting.Script
{
    public class TVM_320 : CsSignalScript
    {
        Dictionary<TVMSpeedType, TVMSpeedType> TAB1 = SNCFV320TAB1;
        Dictionary<TVMSpeedType, TVMSpeedType> TAB2 = SNCFV320TAB2;
        Dictionary<TVMSpeedType, Aspect> MstsTranslation = SNCFV320MstsTranslation;

        TVMSpeedType[] Vpf = new TVMSpeedType[2] { TVMSpeedType._320V, TVMSpeedType._320V };
        TVMSpeedType Vcond = TVMSpeedType._320V;

        public TVM_320()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
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
                Vpf[1] = TVMSpeedType._270V;
            }
            else if (IsSignalFeatureEnabled("USER1"))
            {
                Vpf[1] = TVMSpeedType._300V;
            }
            else
            {
                Vpf[1] = TVMSpeedType._320V;
            }

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
            }
            else
            {
                Vcond = Vpf[0];
                Ve[1] = Min(Ve[1], Vpf[1]);
            }

            Vc[0] = Min(Vcond, Vc[1]);
            Ve[0] = Min(TAB2[Vcond], Ve[1]);
            Va[0] = Va[1];

            if (Va[0] >= Vc[0])
            {
                Va[0] = TVMSpeedType.Any;
            }

            MstsSignalAspect = MstsTranslation[Vc[0]];
            TextSignalAspect = "FR_TVM430"
                + " Ve" + Ve[0].ToString().Substring(1)
                + " Vc" + Vc[0].ToString().Substring(1)
                + (Va[0] != TVMSpeedType.Any ? " Va" + Va[0].ToString().Substring(1) : string.Empty)
                + " Vpf" + Vpf[0].ToString().Substring(1);

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