namespace ORTS.Scripting.Script
{
    public class exAL_RR60 : FrSignalScript
    {
        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.None;
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_RRCLI;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}