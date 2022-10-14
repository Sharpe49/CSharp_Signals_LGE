namespace ORTS.Scripting.Script
{
    public class CHLKennungGleisAbschnittsignal : ChSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Restricting;
            InfoAspect = ChInfoAspect.CH_SIGNAL_SECTION_DE_LIGNE;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}