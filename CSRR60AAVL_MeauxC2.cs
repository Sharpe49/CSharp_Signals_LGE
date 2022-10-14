namespace ORTS.Scripting.Script
{
    public class CSRR60AAVL_MeauxC2 : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisTabGSignalInfo = DeserializeAspect(SignalId, "TABG");
            SignalInfo nextTabGSignalInfo = DeserializeAspect(NextSignalId("TABG"), "TABG");

            if (CommandAspectC(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                if (RouteSet)
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    SignalAspect = SignalAspect.FR_S_BAL;
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.FR_C_BAL;
                }
            }
            else if (nextTabGSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D
                || thisTabGSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D)
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_RR_A;
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.FR_A;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = SignalAspect.FR_ACLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_VL_INF;
                }
            }
            else
            {
                if (AnnounceByA(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = SignalAspect.FR_RRCLI_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_RRCLI;
                }
            }

            FrenchTcs(true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}