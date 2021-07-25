using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR30AAVL : SignalScript
    {
        public CSRR30AAVL()
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
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else if (AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "FR_ACLI";
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
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_RR_A";
                }
                else if (AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "FR_RR_ACLI";
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