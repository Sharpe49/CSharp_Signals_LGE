using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_Id5d : SignalScript
    {
        public RM_Id5d()
        {
        }

        public override void Update()
        {
            string direction = FindSignalAspect("DIR", "INFO", 5);

            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (nextNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_ID_ETEINT";
            }
            else if (direction.Contains("DIR1"))
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_ID_1_FEU";
            }
            else if (direction.Contains("DIR2"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_ID_2_FEUX";
            }
            else if (direction.Contains("DIR3"))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_ID_3_FEUX";
            }
            else if (direction.Contains("DIR4"))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_ID_4_FEUX";
            }
            else if (direction.Contains("DIR5"))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_ID_5_FEUX";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_ID_ETEINT";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}