namespace ORTS.Scripting.Script
{
    public class EOA : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Stop;
            TextSignalAspect = "EOA";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}