namespace ORTS.Scripting.Script
{
    public class ChLEinffrei : ChSignalScript
    {
        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                InfoAspect = ChInfoAspect.None;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                InfoAspect = ChInfoAspect.CH_INFO_IMAGE_1;
            }
            
            SerializeAspect();
        }
    }
}