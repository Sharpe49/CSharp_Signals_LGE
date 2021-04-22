using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class TVM430_CMAVLCLIVL_230E_CAB : CsSignalScript
    {
        public TVM430_CMAVLCLIVL_230E_CAB()
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
                SendSignalMessage(nextNormalSignalId, "FR_TVM430 Vpf230E");
            }

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !nextNormalParts.Contains("FR_TVM430"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C";
            }
            else if (nextNormalParts.Contains("VcRRR"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C";
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
                    TextSignalAspect = "FR_C";
                }
            }
            else if (nextNormalParts.Contains("Vc000")
                && nextNormalParts.Contains("Ve170"))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else if (nextNormalParts.Contains("Ve170"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_VLCLI_EXE";
            }
            else if (nextNormalParts.Contains("Ve230")
                && nextNormalParts.Contains("Vc170"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_VLCLI_ANN";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_SUP";
            }

            TextSignalAspect += " BSP_ECS Vpf230E";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}