namespace ORTS.Scripting.Script
{
    public class KVB_DVL : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_DVL";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}