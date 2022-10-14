namespace ORTS.Scripting.Script
{
    // TECS/TSCS
    public class RM_TCCS : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo ipcsSignalInfo = FindSignalAspect("FR_IPCS", "INFO", 1);

            if (!Enabled || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TECS_TSCS_EFFACE;
            }
            else if (ipcsSignalInfo.IpcsInfoAspect != IpcsInfoAspect.None)
            {
                if (ipcsSignalInfo.IpcsInfoAspect == IpcsInfoAspect.FR_IPCS_ENTREE_CONTRE_SENS)
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_TECS_PRESENTE;
                }
                else if (ipcsSignalInfo.IpcsInfoAspect == IpcsInfoAspect.FR_IPCS_SORTIE_CONTRE_SENS)
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_TSCS_PRESENTE;
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.FR_TECS_TSCS_EFFACE;
                }
            }
            else if (RouteSet)
            {
                // Continuité
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TECS_PRESENTE;
            }
            else
            {
                // Sortie
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TSCS_PRESENTE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}