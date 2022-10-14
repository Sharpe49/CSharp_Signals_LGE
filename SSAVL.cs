namespace ORTS.Scripting.Script
{
    public class SSAVL : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = Script.SignalAspect.FR_SCLI;
            }
            else if (AnnounceByA(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = Script.SignalAspect.FR_A;
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