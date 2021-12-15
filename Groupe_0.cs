namespace ORTS.Scripting.Script
{
    public class Groupe_0 : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.StopAndProceed;
            TextSignalAspect = "GROUPE0";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}