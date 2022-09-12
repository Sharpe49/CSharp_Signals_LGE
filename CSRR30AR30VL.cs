using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR30AR30VL : FrSignalScript
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
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = FrSignalAspect.FR_RR_A;
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts, false, true))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = FrSignalAspect.FR_A;
                }
                else if (AnnounceByR(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = FrSignalAspect.FR_R;
                }
                else if (AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = FrSignalAspect.FR_ACLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = FrSignalAspect.FR_VL_INF;
                }
            }
            else
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = FrSignalAspect.FR_RR_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = FrSignalAspect.FR_RR;
                }
            }

            FrenchTCS(true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}