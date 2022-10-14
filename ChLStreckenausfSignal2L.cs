namespace ORTS.Scripting.Script
{
    public class ChLStreckenausfSignal2L : ChSignalScript
    {
        public override void Update()
        {
            SignalInfo thisDistantSignalInfo = DeserializeAspect(SignalId, "DISTANCE");

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.CH_IMAGE_H;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.CH_IMAGE_1;
            }

            SwissTCS(SignalAspect, thisDistantSignalInfo.Aspect);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}