namespace ORTS.Scripting.Script
{
    public class KVB_DVL : SignalScript
    {
        public KVB_DVL()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_DVL";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}