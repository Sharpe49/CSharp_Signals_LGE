using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR30AVL_BJ : SignalScript
    {
        public CSRR30AVL_BJ()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            string direction = FindSignalAspect("DIR", "INFO", 5);

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
            else if (direction.Contains("DIR7"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
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
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_RR_A";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}