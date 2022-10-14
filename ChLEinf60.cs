namespace ORTS.Scripting.Script
{
    public class ChLEinf60 : ChSignalScript
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
                MstsSignalAspect = Aspect.Approach_2;
                InfoAspect = ChInfoAspect.CH_INFO_IMAGE_3;
            }

            SerializeAspect();
        }
    }
}