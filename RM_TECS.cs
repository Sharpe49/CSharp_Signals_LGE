namespace ORTS.Scripting.Script
{
    public class RM_TECS : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo thisTabGSignalInfo = DeserializeAspect(SignalId, "TABG");
            SignalInfo nextTabGSignalInfo = DeserializeAspect(NextSignalId("TABG"), "TABG");
            SignalInfo IpcsSignalInfo = FindSignalAspect("FR_IPCS", "INFO", 3);

            if (!Enabled
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL
                || nextTabGSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D
                || thisTabGSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TECS_EFFACE;
            }
            else if (IpcsSignalInfo.IpcsInfoAspect != IpcsInfoAspect.None)
            {
                if (IpcsSignalInfo.IpcsInfoAspect == IpcsInfoAspect.FR_IPCS_ENTREE_CONTRE_SENS)
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_TECS_PRESENTE;
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.FR_TECS_EFFACE;
                }
            }
            else if (!RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TECS_PRESENTE;
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TECS_EFFACE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}