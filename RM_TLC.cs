namespace ORTS.Scripting.Script
{
    public class RM_TLC : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TLC_DAMIER;
            }
            else if (nextNormalSignalInfo.Aspect == SignalAspect.FR_M
                || nextNormalSignalInfo.Aspect == SignalAspect.FR_MCLI)
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_TLC_T;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TLC_SLD;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}