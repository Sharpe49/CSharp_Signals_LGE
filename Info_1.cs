namespace ORTS.Scripting.Script
{
    public class Info_1 : SignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.StopAndProceed;
            TextSignalAspect = "DIR1";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}