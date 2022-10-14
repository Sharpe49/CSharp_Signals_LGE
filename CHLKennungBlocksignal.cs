namespace ORTS.Scripting.Script
{
    public class CHLKennungBlocksignal : ChSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Approach_1;
            InfoAspect = ChInfoAspect.CH_SIGNAL_COMBINE;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}