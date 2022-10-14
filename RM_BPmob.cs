namespace ORTS.Scripting.Script
{
    // Tableau Baissez Panto mobile
    public class RM_BPmob : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo directionInfoSignal = FindSignalAspect("DIR", "INFO", 5);

            if (!Enabled || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_BP_ANNONCE_ETEINT;
            }
            // Tableau BP présenté
            else if (!IsSignalFeatureEnabled("USER2") && !IsSignalFeatureEnabled("USER3") && directionInfoSignal.DirectionInfoAspect == DirectionInfoAspect.DIR0)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_BP_ANNONCE_PRESENTE;
            }
            else if (IsSignalFeatureEnabled("USER2") && directionInfoSignal.DirectionInfoAspect == DirectionInfoAspect.DIR1)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_BP_ANNONCE_PRESENTE;
            }
            else if (IsSignalFeatureEnabled("USER3") && directionInfoSignal.DirectionInfoAspect == DirectionInfoAspect.DIR2)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_BP_ANNONCE_PRESENTE;
            }
            // Tableau BP effacé
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_BP_ANNONCE_EFFACE;
            }

            if (IsSignalFeatureEnabled("USER4"))
            {
                FrenchTvm430Bsp();
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}