namespace ORTS.Scripting.Script
{
    public class TVM430_SAVL_FinCAB : FrSignalScript
    {
        public override void Initialize()
        {
            base.Initialize();

            TvmType = TvmType.FR_TVM430;
            VeE = TvmSpeedType._000;
            VcE = TvmSpeedType._RRR;
            VaE = TvmSpeedType.Any;
        }

        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CurrentBlockState != BlockState.Clear)
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
                VeE = TvmSpeedType._160;
                VcE = TvmSpeedType._160E;
            }
            else if (IsSignalFeatureEnabled("USER1")
                && AnnounceByACLI(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.FR_ACLI;
                VeE = TvmSpeedType._160;
                VcE = TvmSpeedType._160E;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_VL_INF;
                VeE = TvmSpeedType._160;
                VcE = TvmSpeedType._160E;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}