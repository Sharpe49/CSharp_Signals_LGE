namespace ORTS.Scripting.Script
{
    public class exAL_CVL_BM : SignalScript
    {
        public exAL_CVL_BM()
        {
        }

        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_VL_INF";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}