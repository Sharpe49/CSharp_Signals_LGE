namespace ORTS.Scripting.Script
{
    public class CSAVL_IPCS : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CurrentBlockState != BlockState.Clear
                || nextNormalSignalInfo.Aspect == SignalAspect.FR_FSO)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_C_BAL;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_VL_INF;
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = !Enabled ? GetDrawState("Off") : DefaultDrawState(MstsSignalAspect);
        }
    }
}