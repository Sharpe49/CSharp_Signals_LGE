namespace ORTS.Scripting.Script
{
    public class RM_CCTa : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "FR_CCT_ANNONCE_PRESENTE";

            if (IsSignalFeatureEnabled("USER1"))
            {
                TextSignalAspect += " EPI_CCT";
            }
            if (IsSignalFeatureEnabled("USER2"))
            {
                TextSignalAspect += " BSP_AODJ";
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