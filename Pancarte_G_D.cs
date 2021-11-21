namespace ORTS.Scripting.Script
{
    public class Pancarte_G_D : SignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_3;
            TextSignalAspect = "FR_TABLEAU_G_D";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}