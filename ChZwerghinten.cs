namespace ORTS.Scripting.Script
{
    public class ChZwerghinten : ChSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;

            if (CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
            }
            else if (nextNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_H)
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