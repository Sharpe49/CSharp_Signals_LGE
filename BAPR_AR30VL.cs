using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class BAPR_AR30VL : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (AnnounceByA(nextNormalParts, false, true))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else if (AnnounceByR(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_R";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_INF";
            }

            TextSignalAspect += FrenchTCS(TextSignalAspect, false, true);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}