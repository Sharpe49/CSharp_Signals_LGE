using System;

namespace ORTS.Scripting.Script
{
    public class CSRR60AAR60VL : FrSignalScript
    {
        // Aspect R and (R) have the same MSTS aspect, so the behaviour has been adapted
        public int DrawStateR = -1;
        public int DrawStateRCLI = -1;
        public int DrawStateRCLI_ACLI = -1;
        public int DrawStateRRCLI_ACLI = -1;

        public override void Initialize()
        {
            base.Initialize();

            DrawStateR = Math.Max(GetDrawState("r30"), GetDrawState("r"));
            DrawStateRCLI = Math.Max(GetDrawState("r60"), GetDrawState("rcli"));
            DrawStateRCLI_ACLI = Math.Max(GetDrawState("r60+aa"), GetDrawState("rcli_acli"));
            DrawStateRRCLI_ACLI = Math.Max(GetDrawState("rr60+aa"), GetDrawState("rrcli_acli"));
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
                if (AnnounceByA(nextNormalSignalInfo, DrawStateR < 0, DrawStateRCLI < 0))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.FR_A;
                }
                else if (DrawStateR >= 0
                    && AnnounceByR(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = SignalAspect.FR_R;
                    DrawState = DrawStateR;
                }
                else if (DrawStateRCLI_ACLI >= 0
                    && IsSignalFeatureEnabled("USER1")
                    && AnnounceByRCLI_ACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = SignalAspect.FR_RCLI_ACLI;
                    DrawState = DrawStateRCLI_ACLI;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = SignalAspect.FR_ACLI;
                }
                else if (DrawStateRCLI >= 0
                    && AnnounceByRCLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = SignalAspect.FR_RCLI;
                    DrawState = DrawStateRCLI;
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
                else if (DrawStateRRCLI_ACLI >= 0
                    && IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = SignalAspect.FR_RRCLI_ACLI;
                    DrawState = DrawStateRRCLI_ACLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
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