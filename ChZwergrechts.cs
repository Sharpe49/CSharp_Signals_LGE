namespace ORTS.Scripting.Script
{
    public class ChZwergrechts : ChSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo nextShuntingSignalInfo = DeserializeAspect(NextSignalId("SHUNTING"), "SHUNTING");

            if (CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
            }
            else if (nextNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_H
                || (nextShuntingSignalInfo.Aspect == SignalAspect.CH_NAIN_FERME && !IsSignalFeatureEnabled("USER1")))
            {
                MstsSignalAspect = Aspect.Stop;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
            }

            TextSignalAspect = "";
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}