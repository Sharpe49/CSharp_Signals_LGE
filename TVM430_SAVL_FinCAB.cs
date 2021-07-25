using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class TVM430_SAVL_FinCAB : SignalScript
    {
        public TVM430_SAVL_FinCAB()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL FR_TVM430 Ve80 Vc000";
            }
            else if (AnnounceByA(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A FR_TVM430 Ve160 Vc160E";
            }
            else if (IsSignalFeatureEnabled("USER1")
                && AnnounceByACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_ACLI FR_TVM430 Ve160 Vc160E";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_INF FR_TVM430 Ve160 Vc160E";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}