namespace ORTS.Scripting.Script
{
    public class Info_2 : SignalScript
    {
        public Info_2()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Restricting;
            TextSignalAspect = "DIR2";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}