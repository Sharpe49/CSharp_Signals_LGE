namespace ORTS.Scripting.Script
{
    public class EOA : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Stop;
            SignalAspect = SignalAspect.EOA;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}