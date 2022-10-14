namespace ORTS.Scripting.Script
{
    public class RM_IdVoieN : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo directionSignalInfo = FindSignalAspect("DIR", "INFO", 5);

            if (CurrentBlockState == BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TIP_ETEINT;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR1)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.FR_TIP_1;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR2)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = SignalAspect.FR_TIP_2;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR3)
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_TIP_3;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR4)
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.FR_TIP_4;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR5)
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_TIP_5;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR6)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TIP_6;
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TIP_ETEINT;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}