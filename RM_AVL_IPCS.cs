namespace ORTS.Scripting.Script
{
    public class RM_AVL_IPCS : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (AnnounceByA(nextNormalSignalInfo, false, false))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_A;
                DrawState = GetDrawState("A");
            }
            else if (IsSignalFeatureEnabled("USER1") && AnnounceByACLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.FR_ACLI;
                DrawState = GetDrawState("ACLI");
            }
            else if (AnnounceByR(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_R;
                DrawState = GetDrawState("R");
            }
            else if (IsSignalFeatureEnabled("USER1") && AnnounceByRCLI_ACLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_RCLI_ACLI;
                DrawState = GetDrawState("RCLI_ACLI");
            }
            else if (AnnounceByRCLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_RCLI;
                DrawState = GetDrawState("RCLI");
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_VL_INF;
                DrawState = GetDrawState("VL");
            }

            FrenchTcs(distantSignal: true);

            SerializeAspect();
            if (!IsSignalFeatureEnabled("USER4") && !Enabled)
            {
                DrawState = GetDrawState("Off");
            }
        }
    }
}