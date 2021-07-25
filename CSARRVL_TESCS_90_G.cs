using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSARRVL_TESCS_90_G : SignalScript
    {
        public CSARRVL_TESCS_90_G()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisRepeaterParts = TextSignalAspectToList(SignalId, "REPEATER");

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed
                || nextNormalParts.Contains("FR_FSO"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                if (RouteSet)
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_C_BAL";
                }
                else
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    TextSignalAspect = "FR_S_BAL";
                }
            }
            else if (nextNormalParts.Contains("FR_TABLEAU_G_D")
                || thisRepeaterParts.Contains("FR_TABLEAU_G_D"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_RR_A";
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_VL_INF";
            }
            else
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_VL_INF";
                }
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}