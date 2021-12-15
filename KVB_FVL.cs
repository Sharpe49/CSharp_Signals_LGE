namespace ORTS.Scripting.Script
{
    public class KVB_FVL : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_FVL";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}