using System;

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
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisTabGSignalInfo = DeserializeAspect(SignalId, "TABG");
            SignalInfo nextTabGSignalInfo = DeserializeAspect(NextSignalId("TABG"), "TABG");
            SignalInfo thisSpeedSignalInfo = DeserializeAspect(SignalId, "TIVR");

            DrawState = -1;

            if (CommandAspectC(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = Script.SignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = Script.SignalAspect.FR_S_BAL;
            }
            else if (nextTabGSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D
                || thisTabGSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = Script.SignalAspect.FR_RR_A;
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = Script.SignalAspect.FR_A;
                }
                else if (IsSignalFeatureEnabled("USER2")
                    && AnnounceByACLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = Script.SignalAspect.FR_ACLI;
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && AnnounceByVLCLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = Script.SignalAspect.FR_VLCLI_ANN;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    if (IsSignalFeatureEnabled("USER3"))
                    {
                        SignalAspect = Script.SignalAspect.FR_VL_SUP;
                    }
                    else
                    {
                        SignalAspect = Script.SignalAspect.FR_VL_INF;
                    }
                }
            }
            else
            {
                if (thisSpeedSignalInfo.Aspect == SignalAspect.FR_RRCLI)
                {
                    if (AnnounceByA(nextNormalSignalInfo))
                    {
                        MstsSignalAspect = Aspect.Approach_1;
                        SignalAspect = Script.SignalAspect.FR_RRCLI_A;
                        DrawState = DrawStateRRCLI_A;
                    }
                    else if (IsSignalFeatureEnabled("USER2")
                        && AnnounceByACLI(nextNormalSignalInfo))
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        SignalAspect = Script.SignalAspect.FR_RRCLI_ACLI;
                        DrawState = DrawStateRRCLI_ACLI;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_1;
                        SignalAspect = Script.SignalAspect.FR_RRCLI;
                        DrawState = DrawStateRRCLI;
                    }
                }
                else
                {
                    if (AnnounceByA(nextNormalSignalInfo))
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        SignalAspect = Script.SignalAspect.FR_RR_A;
                    }
                    else if (DrawStateRR_ACLI >= 0
                        && IsSignalFeatureEnabled("USER2")
                        && AnnounceByACLI(nextNormalSignalInfo))
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        SignalAspect = Script.SignalAspect.FR_RR_ACLI;
                        DrawState = DrawStateRR_ACLI;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Approach_3;
                        SignalAspect = Script.SignalAspect.FR_RR;
                    }
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