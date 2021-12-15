namespace ORTS.Scripting.Script
{
    public class KVB_DGV : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_DGV";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}