using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR60RR30_G : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisTabGParts = TextSignalAspectToList(SignalId, "TABG");
            List<string> nextTabGParts = TextSignalAspectToList(NextSignalId("TABG"), "TABG");

            if (CommandAspectC(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = FrSignalAspect.FR_S_BAL;
            }
            else if (nextTabGParts.Contains("FR_TABLEAU_G_D")
                || thisTabGParts.Contains("FR_TABLEAU_G_D"))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = FrSignalAspect.FR_RR_A;
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = FrSignalAspect.FR_A;
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && AnnounceByVLCLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = FrSignalAspect.FR_VLCLI_ANN;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    if (IsSignalFeatureEnabled("USER3"))
                    {
                        SignalAspect = FrSignalAspect.FR_VL_SUP;
                    }
                    else
                    {
                        SignalAspect = FrSignalAspect.FR_VL_INF;
                    }
                }
            }
            else
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = FrSignalAspect.FR_RRCLI_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = FrSignalAspect.FR_RRCLI;
                }
            }

            FrenchTCS(true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}