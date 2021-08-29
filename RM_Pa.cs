using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    // Tableau P mobile
    public class RM_Pa : SignalScript
    {
        public RM_Pa()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            int nextRepeaterSignalId = NextSignalId("REPEATER");
            string nextRepeaterSignalTextAspect = nextRepeaterSignalId >= 0 ? IdTextSignalAspect(nextRepeaterSignalId, "REPEATER") : string.Empty;

            // Signal répéteur aval non TIVD => Tableau P effacé
            if (nextRepeaterSignalId < 0
                || !nextRepeaterSignalTextAspect.StartsWith("FR_TIVD"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TABP_EFFACE";
            }
            else if (IsSignalFeatureEnabled("USER1"))
            {
                if (RouteSet)
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_TABP_EFFACE";
                }
                else
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_TABP_PRESENTE";
                }
            }
            // Tableau P présenté
            else if (nextNormalParts.Contains("FR_C_BAL")
                || nextNormalParts.Contains("FR_S_BAL")
                || nextNormalParts.Contains("FR_SCLI")
                || nextRepeaterSignalTextAspect == "FR_TIVD_PRESENTE")
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_TABP_PRESENTE";
            }
            // Tableau P effacé
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TABP_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}