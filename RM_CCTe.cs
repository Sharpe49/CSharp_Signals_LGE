namespace ORTS.Scripting.Script
{
    public class RM_CCTe : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            SignalAspect = SignalAspect.FR_CCT_EXECUTION_PRESENTE;

            if (IsSignalFeatureEnabled("USER2"))
            {
                FrenchTvm430Bsp();
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}