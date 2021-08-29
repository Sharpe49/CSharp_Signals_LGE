namespace ORTS.Scripting.Script
{
    public class CHLKennungBhfEnde : SignalScript
    {
        public CHLKennungBhfEnde()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_2;
            TextSignalAspect = "CH_MARQUEUR_DE_SORTIE_DE_GARE";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}