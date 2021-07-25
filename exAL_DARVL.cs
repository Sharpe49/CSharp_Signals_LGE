using System;
using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class exAL_DARVL : SignalScript
    {
        public int DrawStateRCLI_ACLI = -1;

        public exAL_DARVL()
        {
        }

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
                    TextSignalAspect = "FR_D";
                }
                else
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
            }
            else if (AnnounceByA(nextNormalParts, false, true))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else if (AnnounceByR(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_R";
            }
            else if (DrawStateRCLI_ACLI >= 0
                && IsSignalFeatureEnabled("USER2")
                && AnnounceByRCLI_ACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_RCLI_ACLI";
                DrawState = DrawStateRCLI_ACLI;
            }
            else if (IsSignalFeatureEnabled("USER2")
                && AnnounceByACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_ACLI";
            }
            else if (AnnounceByRCLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_RCLI";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_INF";
            }

            if (DrawState < 0)
            {
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
        }
    }
}