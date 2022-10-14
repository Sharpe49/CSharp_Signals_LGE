namespace ORTS.Scripting.Script
{
    public class BAPR_AVL : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (AnnounceByA(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.FR_A;
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