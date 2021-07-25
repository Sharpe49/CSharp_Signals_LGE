namespace ORTS.Scripting.Script
{
    public class RM_OrgVoie : SignalScript
    {
        public RM_OrgVoie()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "";
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}