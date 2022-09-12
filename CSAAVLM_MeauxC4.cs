using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSAAVLM_MeauxC4 : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

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
            else if (nextNormalParts.Contains("FR_TABLEAU_G_D"))
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = FrSignalAspect.FR_M;
            }
            else if (AnnounceByA(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = FrSignalAspect.FR_A;
            }
            else if (AnnounceByACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = FrSignalAspect.FR_ACLI;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = FrSignalAspect.FR_VL_INF;
            }

            FrenchTCS();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}