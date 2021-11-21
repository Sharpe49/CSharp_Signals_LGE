using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class TVM430_CSRR60AVL_FinCAB : SignalScript
    {
        TVMSpeedType Vpf = TVMSpeedType._160E;

        public override void Initialize()
        {
            if (IsSignalFeatureEnabled("USER1"))
            {
                Vpf = TVMSpeedType._130E;
            }
            else
            {
                Vpf = TVMSpeedType._160E;
            }
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL FR_TVM430 Ve80 Vc000";
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL FR_TVM430 Ve80 Vc000";
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts))
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
                if (AnnounceByA(nextNormalParts))
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