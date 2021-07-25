using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class SSAAVLVL : SignalScript
    {
        public SSAAVLVL()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CurrentBlockState != BlockState.Clear)
            {
                if (IsSignalFeatureEnabled("USER1"))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_SCLI";
                }
                else
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    TextSignalAspect = "FR_S_BAL";
                }
            }
            else if (AnnounceByA(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else if (IsSignalFeatureEnabled("USER2")
                && AnnounceByACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_ACLI";
            }
            else if (IsSignalFeatureEnabled("USER3")
                && AnnounceByVLCLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_VLCLI_ANN";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                if (IsSignalFeatureEnabled("USER3"))
                {
                    TextSignalAspect = "FR_VL_SUP";
                }
                else
                {
                    TextSignalAspect = "FR_VL_INF";
                }
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}