namespace ORTS.Scripting.Script
{
    public class CSAVL_TESCS_exAL : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CommandAspectC(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_C_BAL;
            }
            else if (!RouteSet)
            {
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
                else if (IsSignalFeatureEnabled("USER2")
                    && AnnounceByVLCLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_VLCLI_ANN;
                }
                else
                {
                    if (IsSignalFeatureEnabled("USER2"))
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        SignalAspect = SignalAspect.FR_VL_SUP;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        SignalAspect = SignalAspect.FR_VL_INF;
                    }
                }
            }
            else
            {
                if (CurrentBlockState == BlockState.Occupied)
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.FR_C_BAL;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_VL_INF;
                }
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}