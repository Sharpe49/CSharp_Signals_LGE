using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSAAVL_exAL : FrSignalScript
    {
        public override void Update()
        {
            string direction = FindSignalAspect("DIR", "INFO", 5);

            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CommandAspectC(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }
            else if (direction.Contains("DIR1")
                || direction.Contains("DIR2"))
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
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = FrSignalAspect.FR_VL_INF;
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
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = FrSignalAspect.FR_VL_INF;
                }
            }

            FrenchTCS();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}