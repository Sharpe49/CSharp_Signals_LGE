namespace ORTS.Scripting.Script
{
    public class KVB_FVL : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            KvbDivState = KvbDivState.KVB_FVL;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}