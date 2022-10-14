namespace ORTS.Scripting.Script
{
    public class RM_BPr25000V : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            SignalAspect = SignalAspect.FR_BP_FP_25000V_PRESENTE;

            if (IsSignalFeatureEnabled("USER3"))
            {
                FrenchSilec();
            }
            if (IsSignalFeatureEnabled("USER4"))
            {
                FrenchKvbCssp();
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}