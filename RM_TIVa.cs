using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    // TIVD mobile
    public class RM_TIVa : CsSignalScript
    {
        public RM_TIVa()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : string.Empty;
            List<string> nextNormalParts = nextNormalSignalTextAspect.Split(' ').ToList();

            int nextShuntingSignalId = NextSignalId("SHUNTING");
            string nextShuntingSignalTextAspect = nextShuntingSignalId >= 0 ? IdTextSignalAspect(nextShuntingSignalId, "SHUNTING") : string.Empty;

            // Signal aval non trouvé => TIVD présenté
            if (nextNormalSignalId < 0)
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