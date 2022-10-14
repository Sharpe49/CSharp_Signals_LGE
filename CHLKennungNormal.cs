namespace ORTS.Scripting.Script
{
    public class CHLKennungNormal : ChSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Stop;
            InfoAspect = ChInfoAspect.CH_SIGNAL_NORMAL;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}