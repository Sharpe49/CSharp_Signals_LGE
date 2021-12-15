namespace ORTS.Scripting.Script
{
    public class Info_1 : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.StopAndProceed;
            TextSignalAspect = "DIR1";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}