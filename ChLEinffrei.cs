namespace ORTS.Scripting.Script
{
    public class ChLEinffrei : SignalScript
    {
        public ChLEinffrei()
        {
        }

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
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "CH_INFO_IMAGE_1";
            }
        }
    }
}