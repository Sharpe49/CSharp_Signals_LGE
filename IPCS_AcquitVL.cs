namespace ORTS.Scripting.Script
{
    public class IPCS_AcquitVL : FrSignalScript
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

            SignalAspect = SignalAspect.FR_REPRISE_VL;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}