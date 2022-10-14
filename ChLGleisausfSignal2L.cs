namespace ORTS.Scripting.Script
{
    public class ChLGleisausfSignal2L : ChSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisDistantSignalInfo = DeserializeAspect(SignalId, "DISTANCE");
            SignalInfo nextIdentifierSignalInfo = DeserializeAspect(NextSignalId("REPEATER"), "REPEATER");

            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.CH_IMAGE_H;
            }
            else
            {
                if (RouteSet)
                {
                    if (nextNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_H)
                    {
                        if (nextIdentifierSignalInfo.ChInfoAspect == ChInfoAspect.CH_MARQUEUR_DE_SORTIE_DE_GARE)
                        {
                            MstsSignalAspect = Aspect.Stop;
                            SignalAspect = SignalAspect.CH_IMAGE_H;
                        }
                        else
                        {
                            MstsSignalAspect = Aspect.Clear_2;
                            SignalAspect = SignalAspect.CH_IMAGE_1;
                        }
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        SignalAspect = SignalAspect.CH_IMAGE_1;
                    }
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.CH_IMAGE_H;
                }
            }

            SwissTCS(SignalAspect, thisDistantSignalInfo.Aspect);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}