using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class CSRR30AVLVL : CsSignalScript
    {
        public CSRR30AVLVL()
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
            else if (RouteSet)
            {
                if (nextNormalParts.FindAll(x => x == "EOA"
                || x == "FR_C_BAL"
                    || x == "FR_CV"
                    || x == "FR_S_BAL"
                    || x == "FR_S_BAPR"
                    || x == "FR_S_BM"
                    || x == "FR_SCLI"
                    || x == "FR_MCLI"
                    || x == "FR_M"
                    || x == "FR_RR_A"
                    || x == "FR_RR_ACLI"
                    || x == "FR_RR"
                    || x == "FR_RRCLI_A"
                    || x == "FR_RRCLI_ACLI"
                    || x == "FR_RRCLI"
                    ).Count > 0)
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else if (nextNormalParts.FindAll(x => x == "FR_A"
                    || x == "FR_R"
                    || x == "FR_ACLI"
                    || x == "FR_RCLI"
                    || x == "FR_RCLI_ACLI"
                    ).Count > 0)
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "FR_VLCLI_ANN";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_VL_SUP";
                }
            }
            else
            {
                if (nextNormalParts.FindAll(x => x == "EOA"
                || x == "FR_C_BAL"
                    || x == "FR_CV"
                    || x == "FR_S_BAL"
                    || x == "FR_S_BAPR"
                    || x == "FR_S_BM"
                    || x == "FR_SCLI"
                    || x == "FR_MCLI"
                    || x == "FR_M"
                    || x == "FR_RR_A"
                    || x == "FR_RR_ACLI"
                    || x == "FR_RR"
                    || x == "FR_RRCLI_A"
                    || x == "FR_RRCLI_ACLI"
                    || x == "FR_RRCLI"
                ).Count > 0)
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_RR_A";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_RR";
                }
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}