namespace ORTS.Scripting.Script
{
    public class RM_AcquitVL : FrSignalScript
    {
        public override void Update()
        {
            if (CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
            }
            else
            {
                MstsSignalAspect = IdSignalAspect(NextSignalId("NORMAL"), "NORMAL");
            }

            SignalAspect = SignalAspect.FR_REPRISE_VL;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}