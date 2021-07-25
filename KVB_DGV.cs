namespace ORTS.Scripting.Script
{
    public class KVB_DGV : SignalScript
    {
        public KVB_DGV()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_DGV";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}