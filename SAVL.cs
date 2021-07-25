using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class SAVL : SignalScript
    {
        public SAVL()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (AnnounceByA(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_INF";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}