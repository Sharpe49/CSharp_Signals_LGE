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
                InfoAspect = ChInfoAspect.None;
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_3;
                InfoAspect = ChInfoAspect.CH_INFO_IMAGE_5;
            }

            SerializeAspect();
        }
    }
}