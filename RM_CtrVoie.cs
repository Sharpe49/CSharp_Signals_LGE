namespace ORTS.Scripting.Script
{
    public class RM_CtrVoie : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Stop;
            SignalAspect = SignalAspect.FR_FSO;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}