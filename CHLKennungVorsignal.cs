namespace ORTS.Scripting.Script
{
    public class CHLKennungVorsignal : SignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_3;
            TextSignalAspect = "CH_SIGNAL_AVANCE";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}