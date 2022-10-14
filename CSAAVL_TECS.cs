namespace ORTS.Scripting.Script
{
    public class CSAAVL_TECS : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CommandAspectC(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = Script.SignalAspect.FR_C_BAL;
            }
            else if (RouteSet)
            {
                if (CommandAspectS())
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    SignalAspect = Script.SignalAspect.FR_S_BAL;
                }
                else if (AnnounceByA(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = Script.SignalAspect.FR_A;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = Script.SignalAspect.FR_ACLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = Script.SignalAspect.FR_VL_INF;
                }
            }
            else
            {
                // IPCS
                if (CurrentBlockState == BlockState.Occupied)
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = Script.SignalAspect.FR_C_BAL;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = Script.SignalAspect.FR_VL_INF;
                }
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}