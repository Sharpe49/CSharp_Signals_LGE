namespace ORTS.Scripting.Script
{
    public class RM_AcquitVL : SignalScript
    {
        public RM_AcquitVL()
        {
        }

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

            TextSignalAspect = "FR_REPRISE_VL";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}