namespace ORTS.Scripting.Script
{
    public class Plaque_G_D : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo directionSignalInfo = FindSignalAspect("DIR", "INFO", 5);

            if (!Enabled
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL
                || nextNormalSignalInfo.Aspect != SignalAspect.FR_TABLEAU_G_D
                || directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR7)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TABLEAU_G_D_EFFACE;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TABLEAU_G_D_PRESENTE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}