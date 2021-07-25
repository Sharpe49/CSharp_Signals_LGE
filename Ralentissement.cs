using System;
using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class Ralentissement : SignalScript
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
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            DrawState = -1;

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (AnnounceByA(nextNormalParts, DrawStateR < 0, DrawStateRCLI < 0))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else if (DrawStateR >= 0
                && AnnounceByR(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_R";
                DrawState = DrawStateR;
            }
            else if (DrawStateRCLI >= 0
                && AnnounceByRCLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_RCLI";
                DrawState = DrawStateRCLI;
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