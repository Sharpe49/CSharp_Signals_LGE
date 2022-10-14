namespace ORTS.Scripting.Script
{
    public class KVB_DVL : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            KvbDivState = KvbDivState.KVB_DVL;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}