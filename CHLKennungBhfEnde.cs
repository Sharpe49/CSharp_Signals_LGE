namespace ORTS.Scripting.Script
{
    public class CHLKennungBhfEnde : ChSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Approach_2;
            InfoAspect = ChInfoAspect.CH_MARQUEUR_DE_SORTIE_DE_GARE;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}