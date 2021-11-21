using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class IdCS_1_6 : SignalScript
    {
        public override void Update()
        {
            string direction = FindSignalAspect("DIR", "INFO", 5);
            string groupe = FindSignalAspect("GROUPE", "SHUNTING", 5);

            List<string> thisNormalSignalAspect = TextSignalAspectToList(SignalId, "NORMAL");

            if (thisNormalSignalAspect.Contains("FR_C_BAL") || !groupe.Contains("GROUPE0"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_EFFACE";
            }
            else if (direction.Contains("DIR1"))
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_TLD_1";
            }
            else if (direction.Contains("DIR2"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_TLD_2";
            }
            else if (direction.Contains("DIR3"))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_TLD_3";
            }
            else if (direction.Contains("DIR4"))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_TLD_4";
            }
            else if (direction.Contains("DIR5"))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_TLD_5";
            }
            else if (direction.Contains("DIR6"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TLD_6";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}