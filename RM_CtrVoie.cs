namespace ORTS.Scripting.Script
{
    public class RM_CtrVoie : SignalScript
    {
        public RM_CtrVoie()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Stop;
            TextSignalAspect = "FR_FSO";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}