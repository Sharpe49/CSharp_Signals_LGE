using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR60RR30_G : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisTabGParts = TextSignalAspectToList(SignalId, "TABG");
            List<string> nextTabGParts = TextSignalAspectToList(NextSignalId("TABG"), "TABG");

            if (CommandAspectC(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (nextTabGParts.Contains("FR_TABLEAU_G_D")
                || thisTabGParts.Contains("FR_TABLEAU_G_D"))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_RR_A";
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && AnnounceByVLCLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_VLCLI_ANN";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    if (IsSignalFeatureEnabled("USER3"))
                    {
                        TextSignalAspect = "FR_VL_SUP";
                    }
                    else
                    {
                        TextSignalAspect = "FR_VL_INF";
                    }
                }
            }
            else
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_RRCLI_A";
                }
                else
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "FR_RRCLI";
                }
            }

            TextSignalAspect = AddTCS(TextSignalAspect, true);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}