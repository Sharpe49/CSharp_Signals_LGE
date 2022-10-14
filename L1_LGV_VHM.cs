namespace ORTS.Scripting.Script
{
    // TLD
    // L1 - LGV
    public class L1_LGV_VHM : FrSignalScript
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
                SignalAspect = SignalAspect.FR_TLD_EFFACE;
            }
            else if (directionSignalInfo.Aspect == SignalAspect.FR_ID_1_FEU)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TLD_1;
            }
            else if (directionSignalInfo.Aspect == SignalAspect.FR_ID_2_FEUX)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TLD_2;
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