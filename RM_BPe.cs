namespace ORTS.Scripting.Script
{
    public class RM_BPe : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            SignalAspect = SignalAspect.FR_BP_EXECUTION_PRESENTE;

            if (IsSignalFeatureEnabled("USER2"))
            {
                SignalAspect aspect = DeserializeAspect(NextSignalId("BP_FP"), "BP_FP").Aspect;

                if (aspect == SignalAspect.FR_BP_FP_1500V_PRESENTE)
                {
                    Tvm430BspMessage = Tvm430BspMessage.BSP_ELC1_5;
                }
                else if (aspect == SignalAspect.FR_BP_FP_3000V_PRESENTE)
                {
                    Tvm430BspMessage = Tvm430BspMessage.BSP_ELC3;
                }
                else if (aspect == SignalAspect.FR_BP_FP_25000V_PRESENTE)
                {
                    Tvm430BspMessage = Tvm430BspMessage.BSP_ELC25;
                }
                else if (aspect == SignalAspect.FR_BP_FP_25000VLGV_PRESENTE)
                {
                    Tvm430BspMessage = Tvm430BspMessage.BSP_ELGV;
                }
                else if (aspect == SignalAspect.FR_BP_FP_25000VET_PRESENTE)
                {
                    Tvm430BspMessage = Tvm430BspMessage.BSP_EET;
                }
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}