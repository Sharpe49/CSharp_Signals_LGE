namespace ORTS.Scripting.Script
{
    public class RM_OrgVoie : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            SignalAspect = SignalAspect.None;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}
