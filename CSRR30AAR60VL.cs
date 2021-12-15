using System;
using System.Collections.Generic;

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
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            DrawState = -1;

            if (CommandAspectC(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts, true, false))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && DrawStateRCLI_ACLI >= 0
                    && AnnounceByRCLI_ACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "FR_RCLI_ACLI";
                    DrawState = DrawStateRCLI_ACLI;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "FR_ACLI";
                }
                else if (AnnounceByRCLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "FR_RCLI";
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && DrawStateVLCLI >= 0
                    && AnnounceByVLCLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_VLCLI_ANN";
                    DrawState = DrawStateVLCLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    if (IsSignalFeatureEnabled("USER3"))
                    {
                        TextSignalAspect = "FR_VL_SUP";
                    }
                    else
                    {
                        TextSignalAspect = "FR_VL_INF";
                    }
                }
            }
            else
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_RR_A";
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && DrawStateRR_ACLI >= 0
                    && AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_RR_ACLI";
                    DrawState = DrawStateRR_ACLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_RR";
                }
            }

            TextSignalAspect += FrenchTCS(TextSignalAspect, true);

            if (DrawState < 0)
            {
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
        }
    }
}