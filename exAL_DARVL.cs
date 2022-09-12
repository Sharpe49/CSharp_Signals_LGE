using System;
using System.Collections.Generic;

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
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            DrawState = -1;

            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                if (IsSignalFeatureEnabled("USER1"))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = FrSignalAspect.FR_D;
                }
                else
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = FrSignalAspect.FR_A;
                }
            }
            else if (AnnounceByA(nextNormalParts, false, true))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = FrSignalAspect.FR_A;
            }
            else if (AnnounceByR(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = FrSignalAspect.FR_R;
            }
            else if (DrawStateRCLI_ACLI >= 0
                && IsSignalFeatureEnabled("USER2")
                && AnnounceByRCLI_ACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = FrSignalAspect.FR_RCLI_ACLI;
                DrawState = DrawStateRCLI_ACLI;
            }
            else if (IsSignalFeatureEnabled("USER2")
                && AnnounceByACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = FrSignalAspect.FR_ACLI;
            }
            else if (AnnounceByRCLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = FrSignalAspect.FR_RCLI;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = FrSignalAspect.FR_VL_INF;
            }

            FrenchTCS(distantSignal: true);

            SerializeAspect();
            if (DrawState < 0)
            {
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
        }
    }
}