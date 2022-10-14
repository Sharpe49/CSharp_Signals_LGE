using System;

namespace ORTS.Scripting.Script
{
    public class CSRR60AR30VL : FrSignalScript
    {
        public int DrawStateRRCLI_ACLI = -1;
        public int DrawStateVLCLI = -1;

        public override void Initialize()
        {
            base.Initialize();

            DrawStateRRCLI_ACLI = Math.Max(GetDrawState("rr60+aa"), GetDrawState("rrcli_acli"));
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
                if (AnnounceByA(nextNormalSignalInfo, false, true))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.FR_A;
                }
                else if (AnnounceByR(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = SignalAspect.FR_R;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = SignalAspect.FR_ACLI;
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && DrawStateVLCLI >= 0
                    && AnnounceByVLCLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.FR_VLCLI_ANN;
                    DrawState = DrawStateVLCLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
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
                    SignalAspect = SignalAspect.FR_RRCLI_A;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = SignalAspect.FR_RRCLI_ACLI;
                    DrawState = DrawStateRRCLI_ACLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_RRCLI;
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