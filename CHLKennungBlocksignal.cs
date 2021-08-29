namespace ORTS.Scripting.Script
{
    public class CHLKennungBlocksignal : SignalScript
    {
        public CHLKennungBlocksignal()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_1;
            TextSignalAspect = "CH_SIGNAL_COMBINE";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}