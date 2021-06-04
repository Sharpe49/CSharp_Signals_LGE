namespace ORTS.Scripting.Script
{
    public class RM_BPe : SignalScript
    {
        public RM_BPe()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "FR_BP_EXECUTION_PRESENTE";

            if (IsSignalFeatureEnabled("USER2"))
            {
                string aspect = IdTextSignalAspect(NextSignalId("REPEATER"), "REPEATER");

                if (aspect == "FR_BP_FP_1500V_PRESENTE")
                {
                    TextSignalAspect += " BSP_ELC1,5";
                }
                else if (aspect == "FR_BP_FP_3000V_PRESENTE")
                {
                    TextSignalAspect += " BSP_ELC3";
                }
                else if (aspect == "FR_BP_FP_25000V_PRESENTE")
                {
                    TextSignalAspect += " BSP_ELC25";
                }
                else if (aspect == "FR_BP_FP_25000VLGV_PRESENTE")
                {
                    TextSignalAspect += " BSP_ELGV";
                }
                else if (aspect == "FR_BP_FP_25000VET_PRESENTE")
                {
                    TextSignalAspect += " BSP_EET";
                }
            }
            if (IsSignalFeatureEnabled("USER3"))
            {
                TextSignalAspect += " SILEC_BP";
            }
            if (IsSignalFeatureEnabled("USER4"))
            {
                TextSignalAspect += " KVB_BP";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}