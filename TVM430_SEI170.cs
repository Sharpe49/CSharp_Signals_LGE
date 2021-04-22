using Orts.Simulation.Signalling;
using System;
using System.Collections.Generic;
using System.Linq;
using static ORTS.Scripting.Script.TVM430Common;

namespace ORTS.Scripting.Script
{
    public class TVM430_SEI170 : CsSignalScript
    {
        Dictionary<TVMSpeedType, TVMSpeedType> TAB1 = SNCFV320TAB1;
        Dictionary<TVMSpeedType, TVMSpeedType> TAB2 = SNCFV320TAB2;
        Dictionary<TVMSpeedType, Aspect> MstsTranslation = new Dictionary<TVMSpeedType, Aspect>
        {
            { TVMSpeedType.Any, Aspect.StopAndProceed },
            { TVMSpeedType._170E, Aspect.Clear_1 },
            { TVMSpeedType._160, Aspect.Approach_3 },
            { TVMSpeedType._160E, Aspect.Approach_3 },
            { TVMSpeedType._130, Aspect.Approach_2 },
            { TVMSpeedType._130E, Aspect.Approach_2 },
            { TVMSpeedType._80, Aspect.Approach_1 },
            { TVMSpeedType._80E, Aspect.Approach_1 },
            { TVMSpeedType._60, Aspect.Restricting },
            { TVMSpeedType._60E, Aspect.Restricting },
            { TVMSpeedType._000, Aspect.Stop },
            { TVMSpeedType._RRR, Aspect.StopAndProceed }
        };

        TVMSpeedType[] Vpf = new TVMSpeedType[2] { TVMSpeedType._170E, TVMSpeedType._170E };
        TVMSpeedType Vcond = TVMSpeedType._170E;
        bool CNf = false;
        bool RRRAval = false;

        public TVM430_SEI170()
        {
        }

        public override void Initialize()
        {
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

            int nextInfoSignalId = NextSignalId("INFO");
            string nextInfoSignalTextAspect = nextInfoSignalId >= 0 ? IdTextSignalAspect(nextInfoSignalId, "INFO") : string.Empty;
            List<string> nextInfoParts = nextInfoSignalTextAspect.Split(' ').ToList();

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

            TVMSpeedType VeAg = TVMSpeedType._170E;

            if (nextInfoParts.Contains("FR_TVM430_AG"))
            {
                foreach (string part in nextInfoParts)
                {
                    if (part.StartsWith("Ve"))
                    {
                        VeAg = (TVMSpeedType)Enum.Parse(typeof(TVMSpeedType), "_" + part.Substring(2));
                    }
                }
            }

            // Repère Nf fermé => Arret réduit + BSP CNf puis marche à vue (RRR)
            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || Ve[1] == TVMSpeedType.Any
                || Vc[1] == TVMSpeedType.Any)
            {
                Vcond = TVMSpeedType._80E;
                Ve[1] = TVMSpeedType._000;
                Vc[1] = TVMSpeedType._RRR;
                CNf = true;
                RRRAval = true;
            }
            // Entrée sur VS => Arrêt réduit puis marche à vue (RRR)
            else if (!nextNormalParts.Contains("FR_TVM430"))
            {
                Vcond = TVMSpeedType._80E;
                Ve[1] = TVMSpeedType._000;
                Vc[1] = TVMSpeedType._RRR;
                CNf = false;
                RRRAval = true;
            }
            else
            {
                Vcond = Vpf[0];
                if (!RouteSet)
                {
                    Ve[1] = Min(VeAg, Ve[1]);
                }
                Ve[1] = Min(Ve[1], Vpf[1]);
                CNf = false;
                RRRAval = false;
            }

            Vc[0] = Min(Vcond, Ve[1]);
            Ve[0] = Min(TAB2[Vcond], TAB1[Vc[0]]);
            Va[0] = TAB2[Vc[1]];

            if (Va[0] >= Vc[0])
            {
                Va[0] = TVMSpeedType.Any;
            }

            MstsSignalAspect = MstsTranslation[Vc[0]];
            TextSignalAspect = "FR_TVM430"
                + " Ve" + Ve[0].ToString().Substring(1)
                + " Vc" + Vc[0].ToString().Substring(1)
                + (Va[0] != TVMSpeedType.Any ? " Va" + Va[0].ToString().Substring(1) : string.Empty)
                + " Vpf" + Vpf[0].ToString().Substring(1)
                + (CNf ? " BSP_CNf" : string.Empty)
                + (RRRAval ? " RRRAval" : string.Empty);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}