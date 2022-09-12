using System;
using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR60AR30VL : FrSignalScript
    {
        public int DrawStateRRCLI_ACLI = -1;
        public int DrawStateVLCLI = -1;

        public override void Initialize()
        {
            base.Initialize();

            DrawStateRRCLI_ACLI = Math.Max(GetDrawState("rr60+aa"), GetDrawState("rrcli_acli"));
            DrawStateVLCLI = Math.Max(GetDrawState("vlvl"), GetDrawState("vlcli"));
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

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
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts, false, true))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = FrSignalAspect.FR_A;
                }
                else if (AnnounceByR(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = FrSignalAspect.FR_R;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = FrSignalAspect.FR_ACLI;
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && DrawStateVLCLI >= 0
                    && AnnounceByVLCLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = FrSignalAspect.FR_VLCLI_ANN;
                    DrawState = DrawStateVLCLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
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
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = FrSignalAspect.FR_RRCLI_A;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = FrSignalAspect.FR_RRCLI_ACLI;
                    DrawState = DrawStateRRCLI_ACLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = FrSignalAspect.FR_RRCLI;
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