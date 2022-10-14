namespace ORTS.Scripting.Script
{
    public class CvNain : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CommandAspectC(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_CV;
            }
            else if (nextNormalSignalInfo.ESUBO
                && (nextNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL || nextNormalSignalInfo.Aspect == SignalAspect.FR_CV))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_CV;
            }
            else if (IsSignalFeatureEnabled("USER1") && CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_CV;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_M;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}