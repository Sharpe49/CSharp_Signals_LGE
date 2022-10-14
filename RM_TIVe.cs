using System.Text.RegularExpressions;

namespace ORTS.Scripting.Script
{
    // TIVR
    public class RM_TIVe : FrSignalScript
    {
        int SpeedKpH = 0;

        public override void Initialize()
        {
            SpeedKpH = int.Parse(Regex.Match(SignalShapeName, @"[0-9]{2,3}").Value);
        }

        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo speedSignalInfo = FindSignalAspect("FR_VITESSE_AIGUILLE", "INFO", 5);

            SignalInfo signalInfo;
            if (thisNormalSignalInfo.Aspect != SignalAspect.None
                && thisNormalSignalInfo.Aspect != SignalAspect.EOA)
            {
                signalInfo = thisNormalSignalInfo;
            }
            else
            {
                signalInfo = nextNormalSignalInfo;
            }

            if (signalInfo.Aspect == SignalAspect.FR_C_BAL)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_TIVR_ETEINT;
            }
            else if (signalInfo.Aspect == SignalAspect.FR_RR
                || signalInfo.Aspect == SignalAspect.FR_RR_A
                || signalInfo.Aspect == SignalAspect.FR_RR_ACLI
                || signalInfo.Aspect == SignalAspect.FR_RRCLI
                || signalInfo.Aspect == SignalAspect.FR_RRCLI_A
                || signalInfo.Aspect == SignalAspect.FR_RRCLI_ACLI)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TIVR_EFFACE;
            }
            else if (speedSignalInfo.SpeedInfoAspect != SpeedInfoAspect.None)
            {
                if (speedSignalInfo.SpeedInfoAspect != SpeedInfoAspect.FR_VITESSE_AIGUILLE_NON_PARAMETREE)
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_TIVR_PRESENTE;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_TIVR_EFFACE;
                }
            }
            else if (!RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_TIVR_PRESENTE;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_TIVR_EFFACE;
            }

            FrenchKvbTivr(SpeedKpH);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}