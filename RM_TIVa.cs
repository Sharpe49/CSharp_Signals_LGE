using System;
using System.Text.RegularExpressions;

namespace ORTS.Scripting.Script
{
    // TIVD mobile
    public class RM_TIVa : FrSignalScript
    {
        int SpeedKpH = 0;

        public override void Initialize()
        {
            SpeedKpH = int.Parse(Regex.Match(SignalShapeName, @"[0-9]{2,3}").Value);
            SpeedLimitSetByScript = true;
        }

        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            SignalInfo nextTivrSignalInfo = DeserializeAspect(NextSignalId("TIVR"), "TIVR");

            // Signal aval non trouvé => TIVD présenté
            if (nextNormalSignalInfo.Aspect == SignalAspect.None)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TIVD_PRESENTE;
            }
            // Signal aval non équipé de TIVR => TIVD effacé
            else if (nextTivrSignalInfo.Aspect == SignalAspect.None)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TIVD_EFFACE;
            }
            // TIVR éteint ou présenté
            else if (nextNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL
                || nextTivrSignalInfo.Aspect == SignalAspect.FR_TIVR_ETEINT
                || nextTivrSignalInfo.Aspect == SignalAspect.FR_TIVR_PRESENTE)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TIVD_PRESENTE;
            }
            // TIVR effacé
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TIVD_EFFACE;
            }

            FrenchCrocodile();

            if (IsSignalFeatureEnabled("USER3"))
            {
                KvbVraState = KvbVraState.KVB_TPAA;
            }

            if (SignalAspect == SignalAspect.FR_TIVD_PRESENTE)
            {
                FrenchKvbTivd(SpeedKpH);
                SetSpeedLimitKpH(SpeedKpH, Math.Min(SpeedKpH, 100f), false, false, false, true);
            }
            else
            {
                KvbVanState = KvbVanState.None;
                RemoveSpeedLimit();
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}