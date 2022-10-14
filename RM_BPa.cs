namespace ORTS.Scripting.Script
{
    public class RM_BPa : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_2;
            SignalAspect = SignalAspect.FR_BP_ANNONCE_PRESENTE;

            if (IsSignalFeatureEnabled("USER1"))
            {
                FrenchTvm300Epi();
            }
            if (IsSignalFeatureEnabled("USER2"))
            {
                FrenchTvm430Bsp();
            }
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