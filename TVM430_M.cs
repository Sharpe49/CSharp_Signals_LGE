namespace ORTS.Scripting.Script
{
    public class TVM430_M : FrSignalScript
    {
        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.TVM430_FEU_BLANC_ETEINT;
            }
            else
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.TVM430_FEU_BLANC_ALLUME;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}