namespace ORTS.Scripting.Script
{
    public class BAPR_AR30VL : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (AnnounceByA(nextNormalSignalInfo, false, true))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_A;
            }
            else if (AnnounceByR(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.FR_R;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_VL_INF;
            }

            FrenchTcs(false, true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}