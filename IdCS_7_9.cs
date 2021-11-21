using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class IdCS_7_9 : SignalScript
    {
        public override void Update()
        {
            string direction = FindSignalAspect("DIR", "INFO", 5);
            string groupe = FindSignalAspect("GROUPE", "SHUNTING", 5);

            List<string> thisNormalSignalAspect = TextSignalAspectToList(SignalId, "NORMAL");

            if (thisNormalSignalAspect.Contains("FR_C_BAL") || !groupe.Contains("GROUPE1"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_EFFACE";
            }
            else if (direction.Contains("DIR4"))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_TLD_7";
            }
            else if (direction.Contains("DIR5"))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_TLD_8";
            }
            else if (direction.Contains("DIR6"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TLD_9";
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