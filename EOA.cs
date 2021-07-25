namespace ORTS.Scripting.Script
{
    public class EOA : SignalScript
    {
        public EOA()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Stop;
            TextSignalAspect = "EOA";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}