using Orts.Simulation.Signalling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class CSRR30AAR60VL : CsSignalScript
    {
        public int DrawStateRCLI_ACLI = -1;
        public int DrawStateRR_ACLI = -1;

        public CSRR30AAR60VL()
        {

        }

        public override void Initialize()
        {
            DrawStateRCLI_ACLI = Math.Max(GetDrawState("r60+aa"), GetDrawState("rcli_acli"));
            DrawStateRR_ACLI = Math.Max(GetDrawState("rr30+aa"), GetDrawState("rr_acli"));
        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : string.Empty;
            List<string> nextNormalParts = nextNormalSignalTextAspect.Split(' ').ToList();

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
            else if (RouteSet)
            {
                if (nextNormalParts.FindAll(x => x == "FR_C_BAL"
                    || x == "FR_CV"
                    || x == "FR_S_BAL"
                    || x == "FR_S_BAPR"
                    || x == "FR_S_BM"
                    || x == "FR_SCLI"
                    || x == "FR_MCLI"
                    || x == "FR_M"
                    || x == "FR_RR_A"
                    || x == "FR_RR_ACLI"
                    || x == "FR_RR"
                    ).Count > 0)
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                    DrawState = DefaultDrawState(MstsSignalAspect);
                }
                else if (nextNormalParts.Contains("FR_RRCLI_A"))
                {
                    if (IsSignalFeatureEnabled("USER1") && DrawStateRCLI_ACLI >= 0)
                    {
                        MstsSignalAspect = Aspect.Approach_3;
                        TextSignalAspect = "FR_RCLI_ACLI";
                        DrawState = DrawStateRCLI_ACLI;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        TextSignalAspect = "FR_RCLI";
                        DrawState = DefaultDrawState(MstsSignalAspect);
                    }
                }
                else if (nextNormalParts.Contains("FR_RRCLI") || nextNormalParts.Contains("FR_RRCLI_ACLI"))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "FR_RCLI";
                    DrawState = DefaultDrawState(MstsSignalAspect);
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && (nextNormalParts.Contains("FR_A") || nextNormalParts.Contains("FR_R")))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "FR_ACLI";
                    DrawState = DefaultDrawState(MstsSignalAspect);
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_VL_INF";
                    DrawState = DefaultDrawState(MstsSignalAspect);
                }
            }
            else
            {
                if (nextNormalParts.FindAll(x => x == "FR_C_BAL"
                    || x == "FR_CV"
                    || x == "FR_S_BAL"
                    || x == "FR_S_BAPR"
                    || x == "FR_S_BM"
                    || x == "FR_SCLI"
                    || x == "FR_MCLI"
                    || x == "FR_M"
                    || x == "FR_RR_A"
                    || x == "FR_RR_ACLI"
                    || x == "FR_RR"
                    || x == "FR_RRCLI_A"
                    || x == "FR_RRCLI_ACLI"
                    || x == "FR_RRCLI"
                ).Count > 0)
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_RR_A";
                    DrawState = DefaultDrawState(MstsSignalAspect);
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && DrawStateRR_ACLI >= 0
                    && (nextNormalParts.Contains("FR_A") || nextNormalParts.Contains("FR_R")))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_RR_ACLI";
                    DrawState = DrawStateRR_ACLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_RR";
                    DrawState = DefaultDrawState(MstsSignalAspect);
                }
            }
        }
    }
}