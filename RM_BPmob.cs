using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    // Tableau Baissez Panto mobile
    public class RM_BPmob : SignalScript
    {
        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            string direction = FindSignalAspect("DIR", "INFO", 5);

            if (!Enabled || thisNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_BP_ANNONCE_ETEINT";
            }
            // Tableau BP présenté
            else if (!IsSignalFeatureEnabled("USER2") && !IsSignalFeatureEnabled("USER3") && direction.Contains("DIR0"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_BP_ANNONCE_PRESENTE";
            }
            else if (IsSignalFeatureEnabled("USER2") && direction.Contains("DIR1"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_BP_ANNONCE_PRESENTE";
            }
            else if (IsSignalFeatureEnabled("USER3") && direction.Contains("DIR2"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_BP_ANNONCE_PRESENTE";
            }
            // Tableau BP effacé
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_BP_ANNONCE_EFFACE";
            }

            if (IsSignalFeatureEnabled("USER4") && TextSignalAspect == "FR_BP_ANNONCE_PRESENTE")
            {
                TextSignalAspect += " BSP_ABP";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}