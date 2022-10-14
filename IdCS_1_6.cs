namespace ORTS.Scripting.Script
{
    public class IdCS_1_6 : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo directionSignalInfo = FindSignalAspect("DIR", "INFO", 5);
            SignalInfo groupeSignalInfo = FindSignalAspect("GROUPE", "SHUNTING", 5);

            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");

            if (thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL || groupeSignalInfo.DirectionInfoAspect != DirectionInfoAspect.GROUPE0)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TLD_EFFACE;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR1)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.FR_TLD_1;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR2)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = SignalAspect.FR_TLD_2;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR3)
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_TLD_3;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR4)
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.FR_TLD_4;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR5)
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_TLD_5;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR6)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TLD_6;
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TLD_EFFACE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}