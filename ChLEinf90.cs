namespace ORTS.Scripting.Script
{
    public class ChLEinf90 : ChSignalScript
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
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "CH_INFO_IMAGE_5";
            }
        }
    }
}