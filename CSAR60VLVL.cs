namespace ORTS.Scripting.Script
{
    public class CSAR60VLVL : FrSignalScript
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
            else if (AnnounceByA(nextNormalSignalInfo, true, false))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = Script.SignalAspect.FR_A;
            }
            else if (AnnounceByRCLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = Script.SignalAspect.FR_RCLI;
            }
            else if (AnnounceByVLCLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = Script.SignalAspect.FR_VLCLI_ANN;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = Script.SignalAspect.FR_VL_SUP;
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}