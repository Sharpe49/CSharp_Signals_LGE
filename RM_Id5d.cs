namespace ORTS.Scripting.Script
{
    public class RM_Id5d : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo directionSignalInfo = FindSignalAspect("DIR", "INFO", 5);

            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (nextNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_ID_ETEINT;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR1)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.FR_ID_1_FEU;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR2)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = SignalAspect.FR_ID_2_FEUX;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR3)
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_ID_3_FEUX;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR4)
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.FR_ID_4_FEUX;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR5)
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_ID_5_FEUX;
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_ID_ETEINT;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}