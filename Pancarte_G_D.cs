namespace ORTS.Scripting.Script
{
    public class Pancarte_G_D : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Approach_3;
            SignalAspect = SignalAspect.FR_TABLEAU_G_D;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}