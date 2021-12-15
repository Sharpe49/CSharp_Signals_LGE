namespace ORTS.Scripting.Script
{
    public class EOA : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Stop;
            TextSignalAspect = "EOA";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}