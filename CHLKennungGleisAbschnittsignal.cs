namespace ORTS.Scripting.Script
{
    public class CHLKennungGleisAbschnittsignal : SignalScript
    {
        public CHLKennungGleisAbschnittsignal()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Restricting;
            TextSignalAspect = "CH_SIGNAL_SECTION_DE_LIGNE";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}