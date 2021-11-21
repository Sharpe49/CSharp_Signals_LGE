namespace ORTS.Scripting.Script
{
    public class ChLEinf60 : SignalScript
    {
        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "";
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "CH_INFO_IMAGE_3";
            }
        }
    }
}