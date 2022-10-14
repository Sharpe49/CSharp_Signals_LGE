namespace ORTS.Scripting.Script
{
    public class RM_TECS_Id2d : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo directionSignalInfo = FindSignalAspect("DIR", "INFO", 5);
            SignalInfo ipcsSignalInfo = FindSignalAspect("FR_IPCS", "INFO", 3);

            if (!Enabled || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TECS_EFFACE;
            }
            else if (ipcsSignalInfo.IpcsInfoAspect != IpcsInfoAspect.None)
            {
                if (ipcsSignalInfo.IpcsInfoAspect == IpcsInfoAspect.FR_IPCS_ENTREE_CONTRE_SENS)
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = SignalAspect.FR_TECS_PRESENTE;
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.FR_TECS_EFFACE;
                }
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR3 || directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR4)
            {
                MstsSignalAspect = Aspect.Restricting;
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