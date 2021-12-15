using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ORTS.Scripting.Script
{
    // TIVR
    public class RM_TIVe : FrSignalScript
    {
        int SpeedKpH = 0;

        public override void Initialize()
        {
            SpeedKpH = int.Parse(Regex.Match(SignalShapeName, @"[0-9]{2,3}").Value);
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

            if (TextSignalAspect == "FR_TIVR_PRESENTE")
            {
                TextSignalAspect += $" KVB_VRA_V{SpeedKpH}";
            }
            else
            {
                TextSignalAspect += " KVB_VRA_AA";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}