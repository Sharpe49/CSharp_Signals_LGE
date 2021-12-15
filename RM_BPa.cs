namespace ORTS.Scripting.Script
{
    public class RM_BPa : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "FR_BP_ANNONCE_PRESENTE";

            if (IsSignalFeatureEnabled("USER1"))
            {
                TextSignalAspect += " EPI_BPT";
            }
            if (IsSignalFeatureEnabled("USER2"))
            {
                TextSignalAspect += " BSP_ABP";
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