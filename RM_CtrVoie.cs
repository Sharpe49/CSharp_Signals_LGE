namespace ORTS.Scripting.Script
{
    public class RM_CtrVoie : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Stop;
            TextSignalAspect = "FR_FSO";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}