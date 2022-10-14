namespace ORTS.Scripting.Script
{
    public class Rappel_Ralentissement : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo speedSignalInfo = FindSignalAspect("FR_VITESSE_AIGUILLE", "INFO", 5);

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed
                || nextNormalSignalInfo.Aspect == SignalAspect.FR_FSO)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_C_BAL;
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.FR_S_BAL;
            }
            else if (speedSignalInfo.SpeedInfoAspect != SpeedInfoAspect.None)
            {
                if (speedSignalInfo.SpeedInfoAspect == SpeedInfoAspect.FR_VITESSE_AIGUILLE_30)
                {
                    if (AnnounceByA(nextNormalSignalInfo))
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        SignalAspect = SignalAspect.FR_RR_A;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Approach_3;
                        SignalAspect = SignalAspect.FR_RR;
                    }
                }
                else if (speedSignalInfo.SpeedInfoAspect == SpeedInfoAspect.FR_VITESSE_AIGUILLE_60)
                {
                    if (AnnounceByA(nextNormalSignalInfo))
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        SignalAspect = SignalAspect.FR_RRCLI_A;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        SignalAspect = SignalAspect.FR_RRCLI;
                    }
                }
                else
                {
                    if (AnnounceByA(nextNormalSignalInfo))
                    {
                        MstsSignalAspect = Aspect.Approach_1;
                        SignalAspect = SignalAspect.FR_A;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_1;
                        SignalAspect = SignalAspect.FR_VL_INF;
                    }
                }
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.FR_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_VL_INF;
                }
            }
            else
            {
                if (AnnounceByA(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = SignalAspect.FR_RRCLI_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_RRCLI;
                }
            }

            FrenchTcs(true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}