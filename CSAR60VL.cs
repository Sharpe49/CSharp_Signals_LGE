using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSAR60VL : FrSignalScript
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
            else if (AnnounceByA(nextNormalParts, false, false))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = FrSignalAspect.FR_A;
            }
            else if (AnnounceByR(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = FrSignalAspect.FR_R;
            }
            else if (AnnounceByRCLI_ACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = FrSignalAspect.FR_RCLI_ACLI;
            }
            else if (IsSignalFeatureEnabled("USER1")
                && AnnounceByACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = FrSignalAspect.FR_ACLI;
            }
            else if (AnnounceByRCLI(nextNormalParts, IsSignalFeatureEnabled("USER1")))
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = FrSignalAspect.FR_RCLI;
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