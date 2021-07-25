using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_TIDD : SignalScript
    {
        public RM_TIDD()
        {
        }

        public override void Update()
        {
            string direction = FindSignalAspect("FR_ID", "REPEATER", 5);

            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");

            if (thisNormalParts.Contains("FR_C_BAL")
                || thisNormalParts.Contains("FR_S_BAL")
                || thisNormalParts.Contains("FR_SCLI"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TIDD_ETEINT";
            }
            else if (direction.Contains("FR_ID_1_FEU"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TIDD_GAUCHE";
            }
            else if (direction.Contains("FR_ID_2_FEUX"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TIDD_DROITE";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TIDD_ETEINT";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}