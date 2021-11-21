namespace ORTS.Scripting.Script
{
    public class IPCS_AcquitVL : SignalScript
    {
        public override void Update()
        {
            if (CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
            }

            TextSignalAspect = "FR_REPRISE_VL";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}