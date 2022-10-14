namespace ORTS.Scripting.Script
{
    public class CSAAVLM_MeauxC4 : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CommandAspectC(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = Script.SignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = Script.SignalAspect.FR_S_BAL;
            }
            else if (nextNormalSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = Script.SignalAspect.FR_M;
            }
            else if (AnnounceByA(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = Script.SignalAspect.FR_A;
            }
            else if (AnnounceByACLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = Script.SignalAspect.FR_ACLI;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = Script.SignalAspect.FR_VL_INF;
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}