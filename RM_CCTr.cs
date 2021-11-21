namespace ORTS.Scripting.Script
{
    public class RM_CCTr : SignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "FR_CCT_FP_PRESENTE";

            if (IsSignalFeatureEnabled("USER3"))
            {
                TextSignalAspect += " SILEC_FODJA";
            }
            if (IsSignalFeatureEnabled("USER4"))
            {
                TextSignalAspect += " KVB_FODJA";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}