namespace ORTS.Scripting.Script
{
    public class exAL_RR60 : SignalScript
    {
        public exAL_RR60()
        {
        }

        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "";
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_RRCLI";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}