namespace ORTS.Scripting.Script
{
    public class KVB_FZ : SignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_FZ";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}