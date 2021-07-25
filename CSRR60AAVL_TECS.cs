using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR60AAVL_TECS : SignalScript
    {
        public CSRR60AAVL_TECS()
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
                    MstsSignalAspect = Aspect.StopAndProceed;
                    TextSignalAspect = "FR_S_BAL";
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_C_BAL";
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
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "FR_ACLI";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_VL_INF";
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_RRCLI";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}