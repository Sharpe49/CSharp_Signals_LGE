namespace ORTS.Scripting.Script
{
    public class CSRR60AVL_TSCS : FrSignalScript
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
                    if (IsSignalFeatureEnabled("USER1"))
                    {
                        MstsSignalAspect = Aspect.Approach_3;
                        SignalAspect = SignalAspect.FR_RR_A;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        SignalAspect = SignalAspect.FR_RRCLI_A;
                    }
                }
                else
                {
                    if (IsSignalFeatureEnabled("USER1"))
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        SignalAspect = SignalAspect.FR_RR;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        SignalAspect = SignalAspect.FR_RRCLI;
                    }
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_C_BAL;
            }

            FrenchTcs(true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}