namespace ORTS.Scripting.Script
{
    public class ChLEinf40 : SignalScript
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
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "CH_INFO_IMAGE_2";
            }
        }
    }
}