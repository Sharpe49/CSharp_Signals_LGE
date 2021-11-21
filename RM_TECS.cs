using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_TECS : SignalScript
    {
        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            List<string> thisTabGParts = TextSignalAspectToList(SignalId, "TABG");
            List<string> nextTabGParts = TextSignalAspectToList(NextSignalId("TABG"), "TABG");
            string ipcsInformation = FindSignalAspect("FR_IPCS", "INFO", 3);

            if (!Enabled
                || thisNormalParts.Contains("FR_C_BAL")
                || nextTabGParts.Contains("FR_TABLEAU_G_D")
                || thisTabGParts.Contains("FR_TABLEAU_G_D"))
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