namespace ORTS.Scripting.Script
{
    // TLD
    // EO - PE
    // L1 - L4
    // L1 - LGV
    public class EO_PE : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo directionSignalInfo = FindSignalAspect("DIR", "INFO", 5);

            if (thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TLD_EFFACE;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR0)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TLD_1;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR1)
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