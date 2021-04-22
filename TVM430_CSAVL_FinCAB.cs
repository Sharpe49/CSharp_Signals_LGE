using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class TVM430_CSAVL_FinCAB : CsSignalScript
    {
        TVMSpeedType Vpf = TVMSpeedType._220V;

        public TVM430_CSAVL_FinCAB()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            if (IsSignalFeatureEnabled("USER3"))
            {
                Vpf = TVMSpeedType._160E;
            }
            else if (IsSignalFeatureEnabled("USER2"))
            {
                Vpf = TVMSpeedType._200V;
            }
            else if (IsSignalFeatureEnabled("USER1"))
            {
                Vpf = TVMSpeedType._220E;
            }
            else
            {
                Vpf = TVMSpeedType._220V;
            }

            int nextNormalSignalId = NextSignalId("NORMAL");
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : string.Empty;
            List<string> nextNormalParts = nextNormalSignalTextAspect.Split(' ').ToList();

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C FR_TVM430 Ve80 Vc000";
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL FR_TVM430 Ve80 Vc000";
            }
            else if (nextNormalParts.FindAll(x => x == "FR_C"
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
            else if (Vpf == TVMSpeedType._160E)
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_VL_INF FR_TVM430 Ve160 Vc160E";
            }
            else if (Vpf == TVMSpeedType._200V)
            {
                if (nextNormalParts.FindAll(x => x == "FR_A"
                    || x == "FR_R"
                    || x == "FR_ACLI"
                    || x == "FR_RCLI"
                    || x == "FR_RCLI_ACLI"
                    ).Count > 0)
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "FR_VLCLI_ANN FR_TVM430 Ve200 Vc160";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_VL_SUP FR_TVM430 Ve200 Vc200V";
                }
            }
            else
            {
                if (nextNormalParts.FindAll(x => x == "FR_A"
                    || x == "FR_R"
                    || x == "FR_ACLI"
                    || x == "FR_RCLI"
                    || x == "FR_RCLI_ACLI"
                    ).Count > 0)
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "FR_VLCLI_ANN FR_TVM430 Ve220 Vc160";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    if (Vpf == TVMSpeedType._220E)
                    {
                        TextSignalAspect = "FR_VL_SUP FR_TVM430 Ve220 Vc220E";
                    }
                    else
                    {
                        TextSignalAspect = "FR_VL_SUP FR_TVM430 Ve220 Vc220V";
                    }
                }
            }

            TextSignalAspect += " Vpf" + Vpf.ToString().Substring(1);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}