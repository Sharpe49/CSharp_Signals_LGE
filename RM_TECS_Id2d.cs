using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_TECS_Id2d : FrSignalScript
    {
        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            string direction = FindSignalAspect("DIR", "INFO", 5);
            string ipcsInformation = FindSignalAspect("FR_IPCS", "INFO", 3);

            if (!Enabled || thisNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TECS_EFFACE";
            }
            else if (ipcsInformation.Contains("FR_IPCS"))
            {
                if (ipcsInformation.Contains("FR_IPCS_ENTREE_CONTRE_SENS"))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_TECS_PRESENTE";
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_TECS_EFFACE";
                }
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