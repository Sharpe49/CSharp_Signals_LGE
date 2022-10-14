using System;

namespace ORTS.Scripting.Script
{
    public class exAL_DARVL : FrSignalScript
    {
        public int DrawStateRCLI_ACLI = -1;

        public override void Initialize()
        {
            DrawStateRCLI_ACLI = Math.Max(GetDrawState("r60+aa"), GetDrawState("rcli_acli"));
        }

        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            DrawState = -1;

            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                if (IsSignalFeatureEnabled("USER1"))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = SignalAspect.FR_D;
                }
                else
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.FR_A;
                }
            }
            else if (AnnounceByA(nextNormalSignalInfo, false, true))
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
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.FR_RCLI_ACLI;
                DrawState = DrawStateRCLI_ACLI;
            }
            else if (IsSignalFeatureEnabled("USER2")
                && AnnounceByACLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.FR_ACLI;
            }
            else if (AnnounceByRCLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_RCLI;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_VL_INF;
            }

            FrenchTcs(distantSignal: true);

            SerializeAspect();
            if (DrawState < 0)
            {
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
        }
    }
}