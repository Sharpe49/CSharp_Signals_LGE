namespace ORTS.Scripting.Script
{
    public class CvMSAR60VL : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CommandAspectC(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_CV;
            }
            else if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.FR_S_BAL;
            }
            else if (nextNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL || nextNormalSignalInfo.Aspect == SignalAspect.FR_CV)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = SignalAspect.FR_MCLI;
            }
            else if (AnnounceByA(nextNormalSignalInfo, true, false))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_A;
            }
            else if (AnnounceByRCLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_RCLI;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_VL_INF;
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}