using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ORTS.Scripting.Script
{
    // TIVD mobile
    public class RM_TIVa : SignalScript
    {
        int SpeedKpH = 0;

        public override void Initialize()
        {
            SpeedKpH = int.Parse(Regex.Match(SignalShapeName, @"[0-9]{2,3}").Value);
        }

        public override void Update()
        {

            List<string> nextNormalParts = NextNormalSignalTextAspects;

            int nextTIVRId = NextSignalId("TIVR");
            string nextTIVRTextAspect = nextTIVRId >= 0 ? IdTextSignalAspect(nextTIVRId, "TIVR") : string.Empty;

            // Signal aval non trouvé => TIVD présenté
            if (nextNormalParts.Count < 1)
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TIVD_PRESENTE";
            }
            // Signal aval non équipé de TIVR => TIVD effacé
            else if (nextTIVRId < 0
                || !nextTIVRTextAspect.StartsWith("FR_TIVR"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TIVD_EFFACE";
            }
            // TIVR éteint ou présenté
            else if (nextNormalParts.Contains("FR_C_BAL")
                || nextTIVRTextAspect.Contains("FR_TIVR_ETEINT")
                || nextTIVRTextAspect.Contains("FR_TIVR_PRESENTE"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TIVD_PRESENTE";
            }
            // TIVR effacé
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TIVD_EFFACE";
            }

            if (TextSignalAspect.Contains("FR_TIVD_PRESENTE"))
            {
                TextSignalAspect += " CROCODILE_SF";
            }
            else
            {
                TextSignalAspect += " CROCODILE_SO";
            }

            if (IsSignalFeatureEnabled("USER3"))
            {
                TextSignalAspect += " KVB_TPAA";
            }

            if (TextSignalAspect.Contains("FR_TIVD_PRESENTE"))
            {
                TextSignalAspect += $" KVB_VAN_V{SpeedKpH}";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}