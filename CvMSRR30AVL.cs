namespace ORTS.Scripting.Script
{
    public class CvMSRR30AVL : FrSignalScript
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
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.FR_A;
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
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = SignalAspect.FR_RR_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_RR;
                }
            }

            FrenchTcs(distantSignal: true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}