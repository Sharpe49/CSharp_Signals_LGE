namespace ORTS.Scripting.Script
{
    public class CSRR30AVL_BJ : FrSignalScript
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
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.FR_S_BAL;
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
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = SignalAspect.FR_RR_A;
            }

            FrenchTcs(true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}