using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    // Tableau P mobile
    public class RM_Pa : SignalScript
    {
        public override void Initialize()
        {
            SpeedLimitOverriden = true;
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            int nextTIVDId = NextSignalId("TIVD");
            string nextTIVDTextAspect = nextTIVDId >= 0 ? IdTextSignalAspect(nextTIVDId, "TIVD") : string.Empty;

            // Signal répéteur aval non TIVD => Tableau P effacé
            if (nextTIVDId < 0
                || !nextTIVDTextAspect.StartsWith("FR_TIVD"))
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
                || nextTIVDTextAspect == "FR_TIVD_PRESENTE")
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

            if (TextSignalAspect == "FR_TABP_PRESENTE")
            {
                TextSignalAspect += " KVB_VPMOB";
                SetSpeedLimitKpH(160f, 100f, false, false, false, true);
            }
            else
            {
                RemoveSpeedLimit();
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}