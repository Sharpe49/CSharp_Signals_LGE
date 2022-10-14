namespace ORTS.Scripting.Script
{
    public class KVB_FGV : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            KvbDivState = KvbDivState.KVB_FGV;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}