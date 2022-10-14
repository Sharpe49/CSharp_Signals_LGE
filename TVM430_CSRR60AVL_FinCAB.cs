namespace ORTS.Scripting.Script
{
    public class TVM430_CSRR60AVL_FinCAB : FrSignalScript
    {
        TvmSpeedType Vpf = TvmSpeedType._160E;

        public override void Initialize()
        {
            if (IsSignalFeatureEnabled("USER1"))
            {
                Vpf = TvmSpeedType._130E;
            }
            else
            {
                Vpf = TvmSpeedType._160E;
            }

            TvmType = TvmType.FR_TVM430;
            VeE = TvmSpeedType._000;
            VcE = TvmSpeedType._RRR;
            VaE = TvmSpeedType.Any;
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
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalSignalInfo))
                {
                    SignalAspect = SignalAspect.FR_A;

                    if (Vpf == TvmSpeedType._130E)
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        VeE = TvmSpeedType._130;
                        VcE = TvmSpeedType._130E;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Approach_1;
                        VeE = TvmSpeedType._160;
                        VcE = TvmSpeedType._160E;
                    }
                }
                else
                {
                    if (Vpf == TvmSpeedType._130E)
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        SignalAspect = SignalAspect.FR_VL_INF;
                        VeE = TvmSpeedType._130;
                        VcE = TvmSpeedType._130E;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_1;
                        SignalAspect = SignalAspect.FR_VL_INF;
                        VeE = TvmSpeedType._160;
                        VcE = TvmSpeedType._160E;
                    }
                }
            }
            else
            {
                if (AnnounceByA(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = SignalAspect.FR_RRCLI_A;

                    if (Vpf == TvmSpeedType._130E)
                    {
                        VeE = TvmSpeedType._130;
                        VcE = TvmSpeedType._60;
                    }
                    else
                    {
                        VeE = TvmSpeedType._160;
                        VcE = TvmSpeedType._60;
                    }
                }
                else
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = SignalAspect.FR_RRCLI;

                    if (Vpf == TvmSpeedType._130E)
                    {
                        VeE = TvmSpeedType._130;
                        VcE = TvmSpeedType._60;
                    }
                    else
                    {
                        VeE = TvmSpeedType._160;
                        VcE = TvmSpeedType._60;
                    }
                }
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}