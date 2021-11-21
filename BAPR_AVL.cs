using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class BAPR_AVL : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (AnnounceByA(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_INF";
            }

            TextSignalAspect = AddTCS(TextSignalAspect, false, true);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}