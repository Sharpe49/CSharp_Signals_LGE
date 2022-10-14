namespace ORTS.Scripting.Script
{
    public class RM_TSCS : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo ipcsSignalInfo = FindSignalAspect("FR_IPCS", "INFO", 3);

            if (!Enabled || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TSCS_EFFACE;
            }
            else if (ipcsSignalInfo.IpcsInfoAspect != IpcsInfoAspect.None)
            {
                if (ipcsSignalInfo.IpcsInfoAspect == IpcsInfoAspect.FR_IPCS_SORTIE_CONTRE_SENS)
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_TSCS_PRESENTE;
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.FR_TSCS_EFFACE;
                }
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TSCS_PRESENTE;
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TSCS_EFFACE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}