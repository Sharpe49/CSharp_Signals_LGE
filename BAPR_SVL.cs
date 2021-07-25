namespace ORTS.Scripting.Script
{
    public class BAPR_SVL : SignalScript
    {
        public BAPR_SVL()
        {
        }

        public override void Update()
        {
            if (CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAPR";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_INF";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}