using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class TVM430_SAVL_FinCAB : CsSignalScript
    {
        public TVM430_SAVL_FinCAB()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : "EOA";
            List<string> nextNormalParts = nextNormalSignalTextAspect.Split(' ').ToList();

            if (CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL FR_TVM430 Ve80 Vc000";
            }
            else if (nextNormalParts.FindAll(x => x == "FR_C_BAL"
                || x == "FR_CV"
                || x == "FR_S_BAL"
                || x == "FR_S_BAPR"
                || x == "FR_S_BM"
                || x == "FR_SCLI"
                || x == "FR_MCLI"
                || x == "FR_M"
                || x == "FR_RR_A"
                || x == "FR_RR_ACLI"
                || x == "FR_RR"
                || x == "FR_RRCLI_A"
                || x == "FR_RRCLI_ACLI"
                || x == "FR_RRCLI"
                ).Count > 0)
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A FR_TVM430 Ve160 Vc160E";
            }
            else if (IsSignalFeatureEnabled("USER1")
                && nextNormalParts.FindAll(x => x == "FR_A"
                    || x == "FR_R"
                    ).Count > 0)
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_ACLI FR_TVM430 Ve160 Vc160E";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_INF FR_TVM430 Ve160 Vc160E";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}