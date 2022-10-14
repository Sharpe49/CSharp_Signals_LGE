namespace ORTS.Scripting.Script
{
    public class CSMAAR60VL : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo ipcsSignalInfo = FindSignalAspect("FR_IPCS", "INFO", 3);

            if (CommandAspectC(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                if (ipcsSignalInfo.IpcsInfoAspect == IpcsInfoAspect.FR_IPCS_ENTREE_CONTRE_SENS
                    && !IsSignalFeatureEnabled("USER3"))
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.FR_C_BAL;
                }
                else
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    SignalAspect = SignalAspect.FR_S_BAL;
                }
            }
            else if (nextNormalSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = SignalAspect.FR_M;
            }
            else if (ipcsSignalInfo.IpcsInfoAspect == IpcsInfoAspect.FR_IPCS_ENTREE_CONTRE_SENS)
            {
                if (IsSignalFeatureEnabled("USER3")
                    && AnnounceByA(nextNormalSignalInfo, true, false))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.FR_A;
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && AnnounceByRCLI(nextNormalSignalInfo, IsSignalFeatureEnabled("USER2")))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_RCLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_VL_INF;
                }
            }
            else
            {
                if (AnnounceByA(nextNormalSignalInfo, true, false))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.FR_A;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByRCLI_ACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = SignalAspect.FR_RCLI_ACLI;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = SignalAspect.FR_ACLI;
                }
                else if (AnnounceByRCLI(nextNormalSignalInfo, IsSignalFeatureEnabled("USER2")))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_RCLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_VL_INF;
                }
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}