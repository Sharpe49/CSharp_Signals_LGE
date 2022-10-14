namespace ORTS.Scripting.Script
{
    public class SAAVL : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.FR_S_BAL;
            }
            else if (AnnounceByA(nextNormalSignalInfo))
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
            else if (IsSignalFeatureEnabled("USER3")
                && AnnounceByVLCLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_VLCLI_ANN;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                if (IsSignalFeatureEnabled("USER3"))
                {
                    SignalAspect = SignalAspect.FR_VL_SUP;
                }
                else
                {
                    SignalAspect = SignalAspect.FR_VL_INF;
                }
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}