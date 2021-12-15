namespace ORTS.Scripting.Script
{
    public class Info_6 : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_1;
            TextSignalAspect = "DIR6";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}