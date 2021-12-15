namespace ORTS.Scripting.Script
{
    public class CHLKennungBhfEnde : ChSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Approach_2;
            TextSignalAspect = "CH_MARQUEUR_DE_SORTIE_DE_GARE";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}