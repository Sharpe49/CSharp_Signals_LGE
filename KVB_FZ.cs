namespace ORTS.Scripting.Script
{
    public class KVB_FZ : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_FZ";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}