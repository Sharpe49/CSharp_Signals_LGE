using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSAVL_TESCS_exAL : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CommandAspectC(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }
            else if (!RouteSet)
            {
                if (CommandAspectS())
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    SignalAspect = FrSignalAspect.FR_S_BAL;
                }
                else if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = FrSignalAspect.FR_A;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = FrSignalAspect.FR_ACLI;
                }
                else if (IsSignalFeatureEnabled("USER2")
                    && AnnounceByVLCLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = FrSignalAspect.FR_VLCLI_ANN;
                }
                else
                {
                    if (IsSignalFeatureEnabled("USER2"))
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        SignalAspect = FrSignalAspect.FR_VL_SUP;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        SignalAspect = FrSignalAspect.FR_VL_INF;
                    }
                }
            }
            else
            {
                if (CurrentBlockState == BlockState.Occupied)
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = FrSignalAspect.FR_C_BAL;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = FrSignalAspect.FR_VL_INF;
                }
            }

            FrenchTCS();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}