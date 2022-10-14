namespace ORTS.Scripting.Script
{
    public class CSARRVL_TESCS_90_G : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisTabGSignalInfo = DeserializeAspect(SignalId, "TABG");
            SignalInfo nextTabGSignalInfo = DeserializeAspect(NextSignalId("TABG"), "TABG");

            if (CommandAspectC(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = Script.SignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                if (RouteSet)
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = Script.SignalAspect.FR_C_BAL;
                }
                else
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    SignalAspect = Script.SignalAspect.FR_S_BAL;
                }
            }
            else if (nextTabGSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D
                || thisTabGSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = Script.SignalAspect.FR_RR_A;
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = Script.SignalAspect.FR_VL_INF;
            }
            else
            {
                if (AnnounceByA(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = Script.SignalAspect.FR_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = Script.SignalAspect.FR_VL_INF;
                }
            }

            FrenchTcs(true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}