namespace ORTS.Scripting.Script
{
    public class RM_IdVoieN : SignalScript
    {
        public RM_IdVoieN()
        {
        }

        public override void Update()
        {
            string direction = FindSignalAspect("DIR", "INFO", 5);

            if (CurrentBlockState == BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TIP_ETEINT";
            }
            else if (direction.Contains("DIR1"))
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_TIP_1";
            }
            else if (direction.Contains("DIR2"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_TIP_2";
            }
            else if (direction.Contains("DIR3"))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_TIP_3";
            }
            else if (direction.Contains("DIR4"))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_TIP_4";
            }
            else if (direction.Contains("DIR5"))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_TIP_5";
            }
            else if (direction.Contains("DIR6"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TIP_6";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TIP_ETEINT";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}