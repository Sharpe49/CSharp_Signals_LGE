namespace ORTS.Scripting.Script
{
    public class CHLKennungVorsignal : ChSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_3;
            InfoAspect = ChInfoAspect.CH_SIGNAL_AVANCE;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}