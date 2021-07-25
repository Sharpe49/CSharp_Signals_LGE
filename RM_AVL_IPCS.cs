using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_AVL_IPCS : SignalScript
    {
        public RM_AVL_IPCS()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (!Enabled)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_ETEINT_IPCS";
            }
            else if (AnnounceByA(nextNormalParts, false, false))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else if (AnnounceByR(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_R";
            }
            else if (AnnounceByRCLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_RCLI";
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