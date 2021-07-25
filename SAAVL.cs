using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class SAAVL : SignalScript
    {
        public SAAVL()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (AnnounceByA(nextNormalParts))
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

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}