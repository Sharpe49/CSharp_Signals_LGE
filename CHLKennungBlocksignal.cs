namespace ORTS.Scripting.Script
{
    public class CHLKennungBlocksignal : ChSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Approach_1;
            TextSignalAspect = "CH_SIGNAL_COMBINE";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}