namespace ORTS.Scripting.Script
{
    public class Info_2 : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Restricting;
            TextSignalAspect = "DIR2";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}