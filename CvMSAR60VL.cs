using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CvMSAR60VL : SignalScript
    {
        public CvMSAR60VL()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed
                || nextNormalParts.Contains("FR_FSO"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_CV";
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (nextNormalParts.Contains("FR_C_BAL") || nextNormalParts.Contains("FR_CV"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_MCLI";
            }
            else if (AnnounceByA(nextNormalParts, true, false))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else if (AnnounceByRCLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_RCLI";
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