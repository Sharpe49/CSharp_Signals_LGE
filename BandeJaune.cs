namespace ORTS.Scripting.Script
{
    public class BandeJaune : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo directionSignalInfo = FindSignalAspect("DIR", "INFO", 5);
            SignalInfo trackOccupiedSignalInfo = FindSignalAspect("BJ_VOIE", "INFO", 5);

            if (!Enabled
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_S_BAL
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_SCLI)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_BJ_EFFACEE;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR7
                || trackOccupiedSignalInfo.DirectionInfoAspect == DirectionInfoAspect.BJ_VOIE_OCCUPEE)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_BJ_PRESENTEE;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_BJ_EFFACEE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}