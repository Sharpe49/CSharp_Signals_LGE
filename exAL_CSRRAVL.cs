using System;
using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class exAL_CSRRAVL : SignalScript
    {
        public int DrawStateRR_ACLI = -1;
        public int DrawStateRRCLI = -1;
        public int DrawStateRRCLI_A = -1;
        public int DrawStateRRCLI_ACLI = -1;

        public exAL_CSRRAVL()
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            DrawStateRRCLI = Math.Max(GetDrawState("rr30+aa"), GetDrawState("rr_acli"));
            DrawStateRRCLI = Math.Max(GetDrawState("rr60"), GetDrawState("rrcli"));
            DrawStateRRCLI_A = Math.Max(GetDrawState("rr60+a"), GetDrawState("rrcli_a"));
            DrawStateRRCLI_ACLI = Math.Max(GetDrawState("rr60+aa"), GetDrawState("rrcli_acli"));
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisRepeaterParts = TextSignalAspectToList(SignalId, "REPEATER");
            List<string> thisSpeedParts = TextSignalAspectToList(SignalId, "TIVR");

            DrawState = -1;

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed
                || nextNormalParts.Contains("FR_FSO"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (nextNormalParts.Contains("FR_TABLEAU_G_D")
                || thisRepeaterParts.Contains("FR_TABLEAU_G_D"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_RR_A";
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else if (IsSignalFeatureEnabled("USER2")
                    && AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "FR_ACLI";
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && AnnounceByVLCLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_VLCLI_ANN";
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
                if (thisSpeedParts.Contains("FR_RRCLI"))
                {
                    if (AnnounceByA(nextNormalParts))
                    {
                        MstsSignalAspect = Aspect.Approach_1;
                        TextSignalAspect = "FR_RRCLI_A";
                        DrawState = DrawStateRRCLI_A;
                    }
                    else if (IsSignalFeatureEnabled("USER2")
                        && AnnounceByACLI(nextNormalParts))
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        TextSignalAspect = "FR_RRCLI_ACLI";
                        DrawState = DrawStateRRCLI_ACLI;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_1;
                        TextSignalAspect = "FR_RRCLI";
                        DrawState = DrawStateRRCLI;
                    }
                }
                else
                {
                    if (AnnounceByA(nextNormalParts))
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        TextSignalAspect = "FR_RR_A";
                    }
                    else if (DrawStateRR_ACLI >= 0
                        && IsSignalFeatureEnabled("USER2")
                        && AnnounceByACLI(nextNormalParts))
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        TextSignalAspect = "FR_RR_ACLI";
                        DrawState = DrawStateRR_ACLI;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Approach_3;
                        TextSignalAspect = "FR_RR";
                    }
                }
            }

            if (DrawState < 0)
            {
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
        }
    }
}