namespace ORTS.Scripting.Script
{
    public class RM_BPr25000VLGV : SignalScript
    {
        public RM_BPr25000VLGV()
        {
        }

        public override void Initialize()
        {
        }

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