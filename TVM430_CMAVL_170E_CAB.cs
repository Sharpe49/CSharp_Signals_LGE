using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class TVM430_CMAVL_170E_CAB : CsSignalScript
    {
        public TVM430_CMAVL_170E_CAB()
        {
        }

        public override void Initialize()
        {
        }

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
                || CurrentBlockState != BlockState.Clear
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

            TextSignalAspect += " BSP_ECS";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}