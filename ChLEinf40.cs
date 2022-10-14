namespace ORTS.Scripting.Script
{
    public class ChLEinf40 : ChSignalScript
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
                MstsSignalAspect = Aspect.Approach_1;
                InfoAspect = ChInfoAspect.CH_INFO_IMAGE_2;
            }

            SerializeAspect();
        }
    }
}