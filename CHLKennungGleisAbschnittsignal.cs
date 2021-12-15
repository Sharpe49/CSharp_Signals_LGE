namespace ORTS.Scripting.Script
{
    public class CHLKennungGleisAbschnittsignal : ChSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Restricting;
            TextSignalAspect = "CH_SIGNAL_SECTION_DE_LIGNE";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}