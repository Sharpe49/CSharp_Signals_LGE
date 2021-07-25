using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_TSCS_Id2d : SignalScript
    {
        public RM_TSCS_Id2d()
        {
        }

        public override void Update()
        {
            string direction = FindSignalAspect("DIR", "INFO", 5);

            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");

            if (!Enabled || thisNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TSCS_EFFACE";
            }
            else if (direction.Contains("DIR0") || direction.Contains("DIR1") || direction.Contains("DIR2"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_TSCS_PRESENTE";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TSCS_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}