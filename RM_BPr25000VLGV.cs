namespace ORTS.Scripting.Script
{
    public class RM_BPr25000VLGV : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "FR_BP_FP_25000VLGV_PRESENTE";

            if (IsSignalFeatureEnabled("USER3"))
            {
                TextSignalAspect += " SILEC_LP";
            }
            if (IsSignalFeatureEnabled("USER4"))
            {
                TextSignalAspect += " KVB_LP";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}