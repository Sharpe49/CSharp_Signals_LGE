namespace ORTS.Scripting.Script
{
    public class Info_1 : SignalScript
    {
        public Info_1()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.StopAndProceed;
            TextSignalAspect = "DIR1";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}