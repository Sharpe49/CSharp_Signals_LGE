namespace ORTS.Scripting.Script
{
    public class KVB_FGV : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_FGV";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}