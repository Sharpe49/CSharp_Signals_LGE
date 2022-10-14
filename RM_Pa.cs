namespace ORTS.Scripting.Script
{
    // Tableau P mobile
    public class RM_Pa : FrSignalScript
    {
        public override void Initialize()
        {
            SpeedLimitSetByScript = true;
        }

        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            SignalInfo nextTIVDSignalInfo = DeserializeAspect(NextSignalId("TIVD"), "TIVD");

            // Signal répéteur aval non TIVD => Tableau P effacé
            if (nextTIVDSignalInfo.Aspect != SignalAspect.None)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TABP_EFFACE;
            }
            else if (IsSignalFeatureEnabled("USER1"))
            {
                if (RouteSet)
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_TABP_EFFACE;
                }
                else
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.FR_TABP_PRESENTE;
                }
            }
            // Tableau P présenté
            else if (nextNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL
                || nextNormalSignalInfo.Aspect == SignalAspect.FR_S_BAL
                || nextNormalSignalInfo.Aspect == SignalAspect.FR_SCLI
                || nextTIVDSignalInfo.Aspect == SignalAspect.FR_TIVD_PRESENTE)
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_TABP_PRESENTE;
            }
            // Tableau P effacé
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TABP_EFFACE;
            }

            if (SignalAspect == SignalAspect.FR_TABP_PRESENTE)
            {
                KvbVanState = KvbVanState.KVB_VPMOB;
                SetSpeedLimitKpH(160f, 100f, false, false, false, true);
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