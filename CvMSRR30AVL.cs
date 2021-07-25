using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CvMSRR30AVL : SignalScript
    {
        public CvMSRR30AVL()
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
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_VL_INF";
                }
            }
            else
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "FR_RR_A";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_RR";
                }
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}