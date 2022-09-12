using System;
using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class exAL_CSRRAVL : FrSignalScript
    {
        public int DrawStateRR_ACLI = -1;
        public int DrawStateRRCLI = -1;
        public int DrawStateRRCLI_A = -1;
        public int DrawStateRRCLI_ACLI = -1;

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
            List<string> thisTabGParts = TextSignalAspectToList(SignalId, "TABG");
            List<string> nextTabGParts = TextSignalAspectToList(NextSignalId("TABG"), "TABG");
            List<string> thisSpeedParts = TextSignalAspectToList(SignalId, "TIVR");

            DrawState = -1;

            if (CommandAspectC(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = FrSignalAspect.FR_S_BAL;
            }
            else if (nextTabGParts.Contains("FR_TABLEAU_G_D")
                || thisTabGParts.Contains("FR_TABLEAU_G_D"))
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = FrSignalAspect.FR_RR_A;
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = FrSignalAspect.FR_A;
                }
                else if (IsSignalFeatureEnabled("USER2")
                    && AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = FrSignalAspect.FR_ACLI;
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && AnnounceByVLCLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = FrSignalAspect.FR_VLCLI_ANN;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    if (IsSignalFeatureEnabled("USER3"))
                    {
                        SignalAspect = FrSignalAspect.FR_VL_SUP;
                    }
                    else
                    {
                        SignalAspect = FrSignalAspect.FR_VL_INF;
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
                        SignalAspect = FrSignalAspect.FR_RRCLI_A;
                        DrawState = DrawStateRRCLI_A;
                    }
                    else if (IsSignalFeatureEnabled("USER2")
                        && AnnounceByACLI(nextNormalParts))
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        SignalAspect = FrSignalAspect.FR_RRCLI_ACLI;
                        DrawState = DrawStateRRCLI_ACLI;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_1;
                        SignalAspect = FrSignalAspect.FR_RRCLI;
                        DrawState = DrawStateRRCLI;
                    }
                }
                else
                {
                    if (AnnounceByA(nextNormalParts))
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        SignalAspect = FrSignalAspect.FR_RR_A;
                    }
                    else if (DrawStateRR_ACLI >= 0
                        && IsSignalFeatureEnabled("USER2")
                        && AnnounceByACLI(nextNormalParts))
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        SignalAspect = FrSignalAspect.FR_RR_ACLI;
                        DrawState = DrawStateRR_ACLI;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Approach_3;
                        SignalAspect = FrSignalAspect.FR_RR;
                    }
                }
            }

            FrenchTCS(true);

            SerializeAspect();
            if (DrawState < 0)
            {
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
        }
    }
}