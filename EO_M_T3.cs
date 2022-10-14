namespace ORTS.Scripting.Script
{
    // TLD
    // M - T3
    public class EO_M_T3 : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");

            if (thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TLD_EFFACE;
            }
            else if (thisNormalSignalInfo.Aspect == SignalAspect.FR_RR
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_RR_A
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_RR_ACLI)
            {
                if (RouteSet)
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_TLD_1;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_TLD_2;
                }
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