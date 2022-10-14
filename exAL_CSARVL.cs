using System;

namespace ORTS.Scripting.Script
{
    public class exAL_CSARVL : FrSignalScript
    {
        public int DrawStateRCLI = -1;
        public int DrawStateRCLI_ACLI = -1;

        public override void Initialize()
        {
            base.Initialize();

            DrawStateRCLI = Math.Max(GetDrawState("r60"), GetDrawState("rcli"));
            DrawStateRCLI_ACLI = Math.Max(GetDrawState("r60+aa"), GetDrawState("rcli_acli"));
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
                if (IsSignalFeatureEnabled("USER1"))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = SignalAspect.FR_SCLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    SignalAspect = SignalAspect.FR_S_BAL;
                }
            }
            else if (AnnounceByA(nextNormalSignalInfo, false, DrawStateRCLI < 0))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_A;
            }
            else if (AnnounceByR(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_R;
            }
            else if (DrawStateRCLI_ACLI >= 0
                && IsSignalFeatureEnabled("USER2")
                && AnnounceByRCLI_ACLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_RCLI_ACLI;
                DrawState = DrawStateRCLI_ACLI;
            }
            else if (IsSignalFeatureEnabled("USER2")
                && AnnounceByACLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.FR_ACLI;
            }
            else if (DrawStateRCLI >= 0
                && AnnounceByRCLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_RCLI;
                DrawState = DrawStateRCLI;
            }
            else if (IsSignalFeatureEnabled("USER3")
                && AnnounceByVLCLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_VLCLI_ANN;
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

            FrenchTcs();

            SerializeAspect();
            if (DrawState < 0)
            {
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
        }
    }
}