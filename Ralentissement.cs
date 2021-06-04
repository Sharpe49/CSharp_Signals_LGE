using Orts.Simulation.Signalling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class Ralentissement : CsSignalScript
    {
        // Aspect R and (R) have the same MSTS aspect, so the behaviour has been adapted
        public int DrawStateR = -1;
        public int DrawStateRCLI = -1;

        public Ralentissement()
        {

        }

        public override void Initialize()
        {
            DrawStateR = Math.Max(GetDrawState("r30"), GetDrawState("r"));
            DrawStateRCLI = Math.Max(GetDrawState("r60"), GetDrawState("rcli"));
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
            else if (nextNormalParts.FindAll(x => x == "FR_C_BAL"
                || x == "FR_CV"
                || x == "FR_S_BAL"
                || x == "FR_S_BAPR"
                || x == "FR_S_BM"
                || x == "FR_SCLI"
                || x == "FR_MCLI"
                || x == "FR_M"
                ).Count > 0)
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
            else if (nextNormalParts.FindAll(x => x == "FR_RR_A"
                || x == "FR_RR_ACLI"
                || x == "FR_RR"
                ).Count > 0)
            {
                if (DrawStateR >= 0)
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_R";
                    DrawState = DrawStateR;
                }
                else
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                    DrawState = DefaultDrawState(MstsSignalAspect);
                }
            }
            else if (nextNormalParts.FindAll(x => x == "FR_RRCLI"
                || x == "FR_RRCLI_A"
                || x == "FR_RRCLI_ACLI"
                ).Count > 0)
            {
                if (DrawStateRCLI >= 0)
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_RCLI";
                    DrawState = DrawStateRCLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                    DrawState = DefaultDrawState(MstsSignalAspect);
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_INF";
                DrawState = DefaultDrawState(MstsSignalAspect);
            }
        }
    }
}