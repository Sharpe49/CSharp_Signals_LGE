namespace ORTS.Scripting.Script
{
    public class Oeilleton : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");

            if (!Enabled
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAPR
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BM
                || thisNormalSignalInfo.Aspect == SignalAspect.FR_CV)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_OEILLETON_ETEINT;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_OEILLETON_ALLUME;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}