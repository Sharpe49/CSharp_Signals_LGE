namespace ORTS.Scripting.Script
{
    public class RM_Id2d_TECS : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo directionSignalInfo = FindSignalAspect("DIR", "INFO", 5);

            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");

            if (!Enabled || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_ID_ETEINT;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR1
                || directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR3)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.FR_ID_1_FEU;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR2 
                || directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR4)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = SignalAspect.FR_ID_2_FEUX;
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