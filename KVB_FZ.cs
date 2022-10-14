namespace ORTS.Scripting.Script
{
    public class KVB_FZ : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            KvbDivState = KvbDivState.KVB_FZ;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}