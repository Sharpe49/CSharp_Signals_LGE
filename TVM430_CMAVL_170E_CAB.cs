namespace ORTS.Scripting.Script
{
    public class TVM430_CMAVL_170E_CAB : FrSignalScript
    {
        public override void Initialize()
        {
            base.Initialize();

            Tvm430BspMessage = Tvm430BspMessage.BSP_ECS;
        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            if (nextNormalSignalId >= 0)
            {
                SendSignalMessage(nextNormalSignalId, "FR_TVM430 Vpf170E");
            }

            SignalInfo nextNormalSignalInfo = DeserializeAspect(nextNormalSignalId, "NORMAL");

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || nextNormalSignalInfo.TvmType != TvmType.FR_TVM430
                || nextNormalSignalInfo.Ve == TvmSpeedType.None
                || nextNormalSignalInfo.Vc == TvmSpeedType.None
                || nextNormalSignalInfo.Ve == TvmSpeedType.Any
                || nextNormalSignalInfo.Vc == TvmSpeedType.Any)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_C_BAL;
            }
            else if (nextNormalSignalInfo.Vc == TvmSpeedType._RRR)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = SignalAspect.FR_M;
            }
            else if (nextNormalSignalInfo.Ve == TvmSpeedType._60
                || nextNormalSignalInfo.Ve == TvmSpeedType._80)
            {
                if (ApproachControlPosition(100f))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.FR_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.FR_C_BAL;
                }
            }
            else if (nextNormalSignalInfo.Ve == TvmSpeedType._170
                && nextNormalSignalInfo.Vc == TvmSpeedType._000)
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.FR_A;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_VL_INF;
            }
            
            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}