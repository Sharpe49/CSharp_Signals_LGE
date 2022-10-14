namespace ORTS.Scripting.Script
{
    public class ChAbfahrbefehl : ChSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (nextNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_H)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.CH_AUTORISATION_DE_DEPART_EFFACEE;
            }
            else if (IsSignalFeatureEnabled("USER1"))
            {
                if (CurrentBlockState == BlockState.Clear)
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.CH_AUTORISATION_DE_DEPART_EFFACEE;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.CH_AUTORISATION_DE_DEPART_PRESENTEE;
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.CH_AUTORISATION_DE_DEPART_PRESENTEE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}