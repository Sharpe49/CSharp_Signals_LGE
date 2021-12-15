namespace ORTS.Scripting.Script
{
    public class RM_CCTe : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "FR_CCT_EXECUTION_PRESENTE";

            if (IsSignalFeatureEnabled("USER2"))
            {
                TextSignalAspect += " BSP_EODJ";
            }
            if (IsSignalFeatureEnabled("USER3"))
            {
                TextSignalAspect += " SILEC_AODJA";
            }
            if (IsSignalFeatureEnabled("USER4"))
            {
                TextSignalAspect += " KVB_AODJA";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}