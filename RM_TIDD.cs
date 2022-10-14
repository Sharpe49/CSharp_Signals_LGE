namespace ORTS.Scripting.Script
{
    public class RM_TIDD : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo directionSignalInfo = FindSignalAspect("FR_ID", "ID", 1);

            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");

            if (thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_S_BAL
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_SCLI)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TIDD_ETEINT;
            }
            else if (directionSignalInfo.Aspect == SignalAspect.FR_ID_1_FEU)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TIDD_GAUCHE;
            }
            else if (directionSignalInfo.Aspect == SignalAspect.FR_ID_2_FEUX)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TIDD_DROITE;
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TIDD_ETEINT;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}