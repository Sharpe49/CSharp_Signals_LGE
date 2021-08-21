using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    // TIVR
    public class RM_TIVe : SignalScript
    {
        public RM_TIVe()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            string speedInformation = FindSignalAspect("FR_VITESSE_AIGUILLE", "INFO", 5);

            List<string> parts;
            if (thisNormalParts.Count > 0
                && thisNormalParts[0] != ""
                && thisNormalParts[0] != "EOA")
            {
                parts = thisNormalParts;
            }
            else
            {
                parts = nextNormalParts;
            }

            if (parts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TIVR_ETEINT";
            }
            else if (parts.Contains("FR_RR")
                || parts.Contains("FR_RR_A")
                || parts.Contains("FR_RR_ACLI")
                || parts.Contains("FR_RRCLI")
                || parts.Contains("FR_RRCLI_A")
                || parts.Contains("FR_RRCLI_ACLI"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TIVR_EFFACE";
            }
            else if (speedInformation.Contains("FR_VITESSE_AIGUILLE"))
            {
                if (!speedInformation.Contains("FR_VITESSE_AIGUILLE_NON_PARAMETREE"))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_TIVR_PRESENTE";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_TIVR_EFFACE";
                }
            }
            else if (!RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TIVR_PRESENTE";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TIVR_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}