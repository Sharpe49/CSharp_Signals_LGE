namespace ORTS.Scripting.Script
{
    public class KVB_FZ : SignalScript
    {
        public KVB_FZ()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_FZ";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}