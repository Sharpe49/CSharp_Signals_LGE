using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class TVM430_CSAVL_FinCAB : SignalScript
    {
        TVMSpeedType Vpf = TVMSpeedType._220V;

        public TVM430_CSAVL_FinCAB()
        {
        }

        public override void Update()
        {
            if (IsSignalFeatureEnabled("USER4"))
            {
                Vpf = TVMSpeedType._130E;
            }
            else if (IsSignalFeatureEnabled("USER3"))
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
            else if (AnnounceByA(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_1;
                if (Vpf == TVMSpeedType._130E)
                {
                    TextSignalAspect = "FR_A FR_TVM430 Ve130 Vc130E";
                }
                else
                {
                    TextSignalAspect = "FR_A FR_TVM430 Ve160 Vc160E";
                }
            }
            else if (Vpf == TVMSpeedType._130E)
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_VL_INF FR_TVM430 Ve130 Vc130E";
            }
            else if (Vpf == TVMSpeedType._160E)
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_VL_INF FR_TVM430 Ve160 Vc160E";
            }
            else if (Vpf == TVMSpeedType._200V)
            {
                if (AnnounceByVLCLI(nextNormalParts))
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
                if (AnnounceByVLCLI(nextNormalParts))
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