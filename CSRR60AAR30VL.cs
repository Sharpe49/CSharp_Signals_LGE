using System;
using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR60AAR30VL : SignalScript
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
                if (AnnounceByA(nextNormalParts, DrawStateR < 0, DrawStateRCLI < 0))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else if (DrawStateR >= 0
                    && AnnounceByR(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "FR_R";
                    DrawState = DrawStateR;
                }
                else if (DrawStateRCLI_ACLI >= 0
                    && AnnounceByRCLI_ACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "FR_RCLI_ACLI";
                    DrawState = DrawStateRCLI_ACLI;
                }
                else if (AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "FR_ACLI";
                }
                else if (DrawStateRCLI >= 0
                    && AnnounceByRCLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "FR_RCLI";
                    DrawState = DrawStateRCLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_VL_INF";
                }
            }
            else
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_RRCLI_A";
                }
                else if (DrawStateRRCLI_ACLI >= 0
                    && AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_RRCLI_ACLI";
                    DrawState = DrawStateRRCLI_ACLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_RRCLI";
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