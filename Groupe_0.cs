namespace ORTS.Scripting.Script
{
    public class Groupe_0 : SignalScript
    {
        public Groupe_0()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.StopAndProceed;
            TextSignalAspect = "GROUPE0";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}