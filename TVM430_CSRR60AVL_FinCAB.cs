using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class TVM430_CSRR60AVL_FinCAB : CsSignalScript
    {
        TVMSpeedType Vpf = TVMSpeedType._160E;

        public TVM430_CSRR60AVL_FinCAB()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            if (IsSignalFeatureEnabled("USER1"))
            {
                Vpf = TVMSpeedType._130E;
            }
            else
            {
                Vpf = TVMSpeedType._160E;
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
            else if (RouteSet)
            {
                if (nextNormalParts.FindAll(x => x == "FR_C"
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
                    if (Vpf == TVMSpeedType._130E)
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        TextSignalAspect = "FR_A FR_TVM430 Ve130 Vc130E";
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Approach_1;
                        TextSignalAspect = "FR_A FR_TVM430 Ve160 Vc160E";
                    }
                }
                else
                {
                    if (Vpf == TVMSpeedType._130E)
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        TextSignalAspect = "FR_VL_INF FR_TVM430 Ve130 Vc130E";
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_1;
                        TextSignalAspect = "FR_VL_INF FR_TVM430 Ve160 Vc160E";
                    }
                }
            }
            else
            {
                if (nextNormalParts.FindAll(x => x == "FR_C"
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
                    MstsSignalAspect = Aspect.Approach_3;
                    if (Vpf == TVMSpeedType._130E)
                    {
                        TextSignalAspect = "FR_RRCLI_A FR_TVM430 Ve130 Vc60";
                    }
                    else
                    {
                        TextSignalAspect = "FR_RRCLI_A FR_TVM430 Ve160 Vc60";
                    }
                }
                else
                {
                    MstsSignalAspect = Aspect.Restricting;
                    if (Vpf == TVMSpeedType._130E)
                    {
                        TextSignalAspect = "FR_RRCLI FR_TVM430 Ve130 Vc60";
                    }
                    else
                    {
                        TextSignalAspect = "FR_RRCLI FR_TVM430 Ve160 Vc60";
                    }
                }
            }

            TextSignalAspect += " Vpf" + Vpf.ToString().Substring(1);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}