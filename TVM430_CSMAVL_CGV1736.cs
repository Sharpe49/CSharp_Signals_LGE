using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class TVM430_CSMAVL_CGV1736 : SignalScript
    {
        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            List<string> nextNormalParts = new List<string>();
            if (nextNormalSignalId >= 0)
            {
                nextNormalParts = IdTextSignalAspect(nextNormalSignalId, "NORMAL").Split(' ').ToList();
                SendSignalMessage(nextNormalSignalId, "FR_TVM430 Vpf170E");
            }

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }
            else if (RouteSet) // Vers LGV - To HSL
            {
                if (CurrentBlockState == BlockState.Occupied
                    || !nextNormalParts.Contains("FR_TVM430"))
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_C_BAL";
                }
                else if (nextNormalParts.Contains("VcRRR"))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_M";
                }
                else if (nextNormalParts.Contains("Ve60")
                    && nextNormalParts.Contains("Ve80"))
                {
                    if (ApproachControlPosition(100f))
                    {
                        MstsSignalAspect = Aspect.Approach_1;
                        TextSignalAspect = "FR_A";
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Stop;
                        TextSignalAspect = "FR_C_BAL";
                    }
                }
                else if (nextNormalParts.Contains("Vc000")
                    && nextNormalParts.Contains("Ve170"))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "FR_A";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_VL_INF";
                }
            }
            else
            {
                if (CurrentBlockState == BlockState.Occupied)
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
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}