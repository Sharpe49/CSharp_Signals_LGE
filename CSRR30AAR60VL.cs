using System;

namespace ORTS.Scripting.Script
{
    public class CSRR30AAR60VL : FrSignalScript
    {
        public int DrawStateRCLI_ACLI = -1;
        public int DrawStateRR_ACLI = -1;
        public int DrawStateVLCLI = -1;

        public override void Initialize()
        {
            base.Initialize();

            DrawStateRCLI_ACLI = Math.Max(GetDrawState("r60+aa"), GetDrawState("rcli_acli"));
            DrawStateRR_ACLI = Math.Max(GetDrawState("rr30+aa"), GetDrawState("rr_acli"));
            DrawStateVLCLI = Math.Max(GetDrawState("vlvl"), GetDrawState("vlcli"));
        }

        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            DrawState = -1;

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
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalSignalInfo, true, false))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.FR_A;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && DrawStateRCLI_ACLI >= 0
                    && AnnounceByRCLI_ACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = SignalAspect.FR_RCLI_ACLI;
                    DrawState = DrawStateRCLI_ACLI;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = SignalAspect.FR_ACLI;
                }
                else if (AnnounceByRCLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = SignalAspect.FR_RCLI;
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && DrawStateVLCLI >= 0
                    && AnnounceByVLCLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_VLCLI_ANN;
                    DrawState = DrawStateVLCLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    if (IsSignalFeatureEnabled("USER3"))
                    {
                        SignalAspect = SignalAspect.FR_VL_SUP;
                    }
                    else
                    {
                        SignalAspect = SignalAspect.FR_VL_INF;
                    }
                }
            }
            else
            {
                if (AnnounceByA(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = SignalAspect.FR_RR_A;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && DrawStateRR_ACLI >= 0
                    && AnnounceByACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = SignalAspect.FR_RR_ACLI;
                    DrawState = DrawStateRR_ACLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_RR;
                }
            }

            FrenchTcs(true);

            SerializeAspect();
            if (DrawState < 0)
            {
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
        }
    }
}