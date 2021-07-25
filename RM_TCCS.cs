using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    // TECS/TSCS
    public class RM_TCCS : SignalScript
    {
        public RM_TCCS()
        {
        }

        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");

            if (!Enabled || thisNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TECS_EFFACE";
            }
            else if (RouteSet)
            {
                // Continuité
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TECS_PRESENTE";
            }
            else
            {
                // Sortie
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TSCS_PRESENTE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}