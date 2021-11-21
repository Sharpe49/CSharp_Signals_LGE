namespace ORTS.Scripting.Script
{
    public class KVB_FGV : SignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_FGV";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}