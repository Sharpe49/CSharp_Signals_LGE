namespace ORTS.Scripting.Script
{
    public class TVM430_CSAVL_FinCAB : FrSignalScript
    {
        TvmSpeedType Vpf = TvmSpeedType._220V;

        public override void Initialize()
        {
            if (IsSignalFeatureEnabled("USER4"))
            {
                Vpf = TvmSpeedType._130E;
            }
            else if (IsSignalFeatureEnabled("USER3"))
            {
                Vpf = TvmSpeedType._160E;
            }
            else if (IsSignalFeatureEnabled("USER2"))
            {
                Vpf = TvmSpeedType._200V;
            }
            else if (IsSignalFeatureEnabled("USER1"))
            {
                Vpf = TvmSpeedType._220E;
            }
            else
            {
                Vpf = TvmSpeedType._220V;
            }

            TvmType = TvmType.FR_TVM430;
            VeE = TvmSpeedType._000;
            VcE = TvmSpeedType._RRR;
            VaE = TvmSpeedType.Any;
            TvmEndOfBlock = true;
        }

        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_C_BAL;
                VeE = TvmSpeedType._80;
                VcE = TvmSpeedType._000;
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = SignalAspect.FR_S_BAL;
                VeE = TvmSpeedType._80;
                VcE = TvmSpeedType._000;
            }
            else if (AnnounceByA(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_A;

                if (Vpf == TvmSpeedType._130E)
                {
                    VeE = TvmSpeedType._130;
                    VcE = TvmSpeedType._130E;
                }
                else
                {
                    VeE = TvmSpeedType._160;
                    VcE = TvmSpeedType._160E;
                }
            }
            else if (Vpf == TvmSpeedType._130E)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_VL_INF;
                VeE = TvmSpeedType._130;
                VcE = TvmSpeedType._130E;
            }
            else if (Vpf == TvmSpeedType._160E)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_VL_INF;
                VeE = TvmSpeedType._160;
                VcE = TvmSpeedType._160E;
            }
            else if (Vpf == TvmSpeedType._200V)
            {
                if (AnnounceByVLCLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = SignalAspect.FR_VLCLI_ANN;
                    VeE = TvmSpeedType._200;
                    VcE = TvmSpeedType._160;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.FR_VL_SUP;
                    VeE = TvmSpeedType._200;
                    VcE = TvmSpeedType._200V;
                }
            }
            else
            {
                if (AnnounceByVLCLI(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = SignalAspect.FR_VLCLI_ANN;
                    VeE = TvmSpeedType._220;
                    VcE = TvmSpeedType._160;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    if (Vpf == TvmSpeedType._220E)
                    {
                        SignalAspect = SignalAspect.FR_VL_SUP;
                        VeE = TvmSpeedType._220;
                        VcE = TvmSpeedType._220E;
                    }
                    else
                    {
                        SignalAspect = SignalAspect.FR_VL_SUP;
                        VeE = TvmSpeedType._220;
                        VcE = TvmSpeedType._220V;
                    }
                }
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}