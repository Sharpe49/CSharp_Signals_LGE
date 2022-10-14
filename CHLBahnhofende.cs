namespace ORTS.Scripting.Script
{
    public class CHLBahnhofende : ChSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo nextIdentifierSignalInfo = DeserializeAspect(NextSignalId("REPEATER"), "REPEATER");

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.CH_IMAGE_H;
                InfoAspect = ChInfoAspect.None;
            }
            else if (nextIdentifierSignalInfo.ChInfoAspect == ChInfoAspect.CH_SIGNAL_AVANCE)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.CH_IMAGE_1;
                InfoAspect = ChInfoAspect.None;
            }
            else
            {
                if (nextNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_H
                    || nextNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_6)
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = SignalAspect.CH_IMAGE_1;
                    InfoAspect = ChInfoAspect.CH_PROCHAIN_SIGNAL_FERME;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.CH_IMAGE_1;
                    InfoAspect = ChInfoAspect.None;
                }
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}