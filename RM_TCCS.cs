using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    // TECS/TSCS
    public class RM_TCCS : SignalScript
    {
        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            string ipcsInformation = FindSignalAspect("FR_IPCS", "INFO", 1);

            if (!Enabled || thisNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TECS_TSCS_EFFACE";
            }
            else if (ipcsInformation.Contains("FR_IPCS"))
            {
                if (ipcsInformation.Contains("FR_IPCS_ENTREE_CONTRE_SENS"))
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_TECS_PRESENTE";
                }
                else if (ipcsInformation.Contains("FR_IPCS_SORTIE_CONTRE_SENS"))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_TSCS_PRESENTE";
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_TECS_TSCS_EFFACE";
                }
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