namespace ORTS.Scripting.Script
{
    public class SSAVLVL : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.FR_SCLI;
            }
            else if (AnnounceByA(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_A;
            }
            else if (AnnounceByVLCLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_VLCLI_ANN;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_VL_SUP;
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}