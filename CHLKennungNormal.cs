namespace ORTS.Scripting.Script
{
    public class CHLKennungNormal : SignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Stop;
            TextSignalAspect = "CH_SIGNAL_NORMAL";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}