namespace ORTS.Scripting.Script
{
    public class CVM_horiz : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || nextNormalSignalInfo.Aspect == SignalAspect.FR_FSO)
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
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_M;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}