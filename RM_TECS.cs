using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_TECS : SignalScript
    {
        public RM_TECS()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            List<string> thisRepeaterParts = TextSignalAspectToList(SignalId, "REPEATER");
            string ipcsInformation = FindSignalAspect("FR_IPCS", "INFO", 3);

            if (!Enabled
                || thisNormalParts.Contains("FR_C_BAL")
                || nextNormalParts.Contains("FR_TABLEAU_G_D")
                || thisRepeaterParts.Contains("FR_TABLEAU_G_D"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TECS_EFFACE";
            }
            else if (ipcsInformation.Contains("FR_IPCS"))
            {
                if (ipcsInformation.Contains("FR_IPCS_ENTREE_CONTRE_SENS"))
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_TECS_PRESENTE";
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_TECS_EFFACE";
                }
            }
            else if (!RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_2;
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