namespace ORTS.Scripting.Script
{
    public class BM_C : FrSignalScript
    {
        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_C_BM;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_VL_INF;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}