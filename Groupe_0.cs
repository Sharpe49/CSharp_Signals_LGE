namespace ORTS.Scripting.Script
{
    public class Groupe_0 : SignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.StopAndProceed;
            TextSignalAspect = "GROUPE0";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}