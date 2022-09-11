using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_AVL_IPCS : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (AnnounceByA(nextNormalParts, false, false))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
                DrawState = GetDrawState("A");
            }
            else if (IsSignalFeatureEnabled("USER1") && AnnounceByACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_ACLI";
                DrawState = GetDrawState("ACLI");
            }
            else if (AnnounceByR(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_R";
                DrawState = GetDrawState("R");
            }
            else if (IsSignalFeatureEnabled("USER1") && AnnounceByRCLI_ACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_RCLI_ACLI";
                DrawState = GetDrawState("RCLI_ACLI");
            }
            else if (AnnounceByRCLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_RCLI";
                DrawState = GetDrawState("RCLI");
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_VL_INF";
                DrawState = GetDrawState("VL");
            }

            if (!IsSignalFeatureEnabled("USER4") && !Enabled)
            {
                DrawState = GetDrawState("Off");
            }
        }
    }
}