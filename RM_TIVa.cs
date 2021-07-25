using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    // TIVD mobile
    public class RM_TIVa : SignalScript
    {
        public RM_TIVa()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            int nextShuntingSignalId = NextSignalId("SHUNTING");
            string nextShuntingSignalTextAspect = nextShuntingSignalId >= 0 ? IdTextSignalAspect(nextShuntingSignalId, "SHUNTING") : string.Empty;

            // Signal aval non trouvé => TIVD présenté
            if (nextNormalParts.Count < 1)
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TIVD_PRESENTE";
            }
            // Signal aval non équipé de TIVR => TIVD effacé
            else if (nextShuntingSignalId < 0
                || !nextShuntingSignalTextAspect.StartsWith("FR_TIVR"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TIVD_EFFACE";
            }
            // TIVR éteint ou présenté
            else if (nextNormalParts.Contains("FR_C_BAL")
                || nextShuntingSignalTextAspect == "FR_TIVR_ETEINT"
                || nextShuntingSignalTextAspect == "FR_TIVR_PRESENTE")
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

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}