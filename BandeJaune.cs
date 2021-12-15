using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class BandeJaune : FrSignalScript
    {
        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            string directionInformation = FindSignalAspect("DIR", "INFO", 5);
            string trackOccupiedInformation = FindSignalAspect("BJ_VOIE", "INFO", 5);

            if (!Enabled
                || thisNormalParts.Contains("FR_C_BAL")
                || thisNormalParts.Contains("FR_S_BAL")
                || thisNormalParts.Contains("FR_SCLI"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_BJ_EFFACEE";
            }
            else if (directionInformation.Contains("DIR7")
                || trackOccupiedInformation.Contains("BJ_VOIE_OCCUPEE"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_BJ_PRESENTEE";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_BJ_EFFACEE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}