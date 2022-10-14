namespace ORTS.Scripting.Script
{
    public class IdCS_7_9 : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo directionSignalInfo = FindSignalAspect("DIR", "INFO", 5);
            SignalInfo groupeSignalInfo = FindSignalAspect("GROUPE", "SHUNTING", 5);

            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");

            if (thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL || groupeSignalInfo.DirectionInfoAspect != DirectionInfoAspect.GROUPE1)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TLD_EFFACE;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR4)
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.FR_TLD_7;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR5)
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_TLD_8;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR6)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TLD_9;
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