using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_TSCS : SignalScript
    {
        public RM_TSCS()
        {
        }

        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            string ipcsInformation = FindSignalAspect("FR_IPCS", "INFO", 3);

            if (!Enabled || thisNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TSCS_EFFACE";
            }
            else if (ipcsInformation.Contains("FR_IPCS"))
            {
                if (ipcsInformation.Contains("FR_IPCS_SORTIE_CONTRE_SENS"))
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_TSCS_PRESENTE";
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_TSCS_EFFACE";
                }
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_2;
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