namespace ORTS.Scripting.Script
{
    public class BM_Cv : FrSignalScript
    {
        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_CV;
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_M;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}