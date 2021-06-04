using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class CSAR60VL : CsSignalScript
    {
        public CSAR60VL()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : "EOA";
            List<string> nextNormalParts = nextNormalSignalTextAspect.Split(' ').ToList();

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (nextNormalParts.FindAll(x => x == "EOA"
                || x == "FR_C_BAL"
                || x == "FR_CV"
                || x == "FR_S_BAL"
                || x == "FR_S_BAPR"
                || x == "FR_S_BM"
                || x == "FR_SCLI"
                || x == "FR_MCLI"
                || x == "FR_M"
                ).Count > 0)
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else if (nextNormalParts.FindAll(x => x == "FR_RR"
                || x == "FR_RR_A"
                || x == "FR_RR_ACLI"
                ).Count > 0)
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_R";
            }
            else if (nextNormalParts.Contains("FR_RRCLI_A"))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_RCLI_ACLI";
            }
            else if (nextNormalParts.Contains("FR_A") ||
                nextNormalParts.Contains("FR_R"))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_ACLI";
            }
            else if (nextNormalParts.Contains("FR_RRCLI")
                || nextNormalParts.Contains("FR_RRCLI_ACLI"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_RCLI";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_INF";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}