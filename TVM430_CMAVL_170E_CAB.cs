using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class TVM430_CMAVL_170E_CAB : FrSignalScript
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
                || CurrentBlockState != BlockState.Clear
                || !nextNormalParts.Contains("FR_TVM430"))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }
            else if (nextNormalParts.Contains("VcRRR"))
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = FrSignalAspect.FR_M;
            }
            else if (nextNormalParts.Contains("Ve60")
                && nextNormalParts.Contains("Ve80"))
            {
                if (ApproachControlPosition(100f))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = FrSignalAspect.FR_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = FrSignalAspect.FR_C_BAL;
                }
            }
            else if (nextNormalParts.Contains("Vc000")
                && nextNormalParts.Contains("Ve170"))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = FrSignalAspect.FR_A;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = FrSignalAspect.FR_VL_INF;
            }

            SerializeAspect();
            TextSignalAspect += " BSP_ECS";
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}