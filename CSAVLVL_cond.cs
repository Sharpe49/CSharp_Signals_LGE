namespace ORTS.Scripting.Script
{
    public class CSAVLVL_cond : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisTIVRSignalInfo = DeserializeAspect(SignalId, "TIVR");

            if (CommandAspectC(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.FR_S_BAL;
            }
            else if (AnnounceByA(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_A;
            }
            else if (IsSignalFeatureEnabled("USER1")
                && AnnounceByACLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.FR_ACLI;
            }
            else if (thisTIVRSignalInfo.Aspect == SignalAspect.FR_TIVR_EFFACE
                && AnnounceByVLCLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_VLCLI_ANN;
            }
            else
            {
                if (thisTIVRSignalInfo.Aspect == SignalAspect.FR_TIVR_EFFACE)
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_VL_SUP;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_VL_INF;
                }
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}