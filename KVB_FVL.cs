namespace ORTS.Scripting.Script
{
    public class KVB_FVL : SignalScript
    {
        public KVB_FVL()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_FVL";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}