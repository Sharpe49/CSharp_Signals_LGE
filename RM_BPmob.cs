using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    // Tableau Baissez Panto mobile
    public class RM_BPmob : SignalScript
    {
        public RM_BPmob()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            string thisNormalSignalTextAspect = IdTextSignalAspect(SignalId, "NORMAL");
            List<string> thisNormalParts = thisNormalSignalTextAspect.Split(' ').ToList();

            string direction = FindSignalAspect("DIR", "INFO", 5);

            if (!Enabled || thisNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_BP_ANNONCE_ETEINT";
            }
            // Tableau BP pr�sent�
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
            // Tableau BP effac�
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