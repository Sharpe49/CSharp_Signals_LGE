namespace ORTS.Scripting.Script
{
    public class CSRR30AVL : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CommandAspectC(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                if (IsSignalFeatureEnabled("USER2")
                    && !TrainHasCallOn())
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.FR_C_BAL;
                }
                else
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    SignalAspect = SignalAspect.FR_S_BAL;
                }
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
                    SignalAspect = SignalAspect.FR_RR_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_RR;
                }
            }

            FrenchTcs(true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}