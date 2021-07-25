using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_TECS_Id2d : SignalScript
    {
        public RM_TECS_Id2d()
        {
        }

        public override void Update()
        {
            string direction = FindSignalAspect("DIR", "INFO", 5);

            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");

            if (!Enabled || thisNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TECS_EFFACE";
            }
            else if (direction.Contains("DIR3") || direction.Contains("DIR4"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_TECS_PRESENTE";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TECS_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}