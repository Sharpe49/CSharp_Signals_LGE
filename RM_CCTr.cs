namespace ORTS.Scripting.Script
{
    public class RM_CCTr : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            SignalAspect = SignalAspect.FR_CCT_FP_PRESENTE;

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