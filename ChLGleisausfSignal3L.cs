namespace ORTS.Scripting.Script
{
    public class ChLGleisausfSignal3L : ChSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisDistantSignalInfo = DeserializeAspect(SignalId, "DISTANCE");
            SignalInfo thisInfoSignalInfo = DeserializeAspect(SignalId, "INFO");
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
                    if (thisDistantSignalInfo.Aspect != SignalAspect.None
                        && (nextNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_H
                            || nextNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_6))
                    {
                        if (nextIdentifierSignalInfo.ChInfoAspect == ChInfoAspect.CH_MARQUEUR_DE_SORTIE_DE_GARE)
                        {
                            MstsSignalAspect = Aspect.Stop;
                            SignalAspect = SignalAspect.CH_IMAGE_H;
                        }
                        else
                        {
                            MstsSignalAspect = Aspect.Approach_1;
                            SignalAspect = SignalAspect.CH_IMAGE_2;
                        }
                    }
                    else if (IsSignalFeatureEnabled("USER2"))
                    {
                        MstsSignalAspect = Aspect.Approach_1;
                        SignalAspect = SignalAspect.CH_IMAGE_2;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        SignalAspect = SignalAspect.CH_IMAGE_1;
                    }
                }
                else
                {
                    if (thisInfoSignalInfo.ChInfoAspect == ChInfoAspect.CH_INFO_IMAGE_2
                        || thisInfoSignalInfo.ChInfoAspect == ChInfoAspect.CH_INFO_IMAGE_3
                        || thisInfoSignalInfo.ChInfoAspect == ChInfoAspect.CH_INFO_IMAGE_5
                        || thisInfoSignalInfo.ChInfoAspect == ChInfoAspect.CH_INFO_IMAGE_6)
                    {
                        MstsSignalAspect = Aspect.Approach_1;
                        SignalAspect = SignalAspect.CH_IMAGE_2;
                    }
                    else if (thisInfoSignalInfo.ChInfoAspect == ChInfoAspect.CH_INFO_IMAGE_1)
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        SignalAspect = SignalAspect.CH_IMAGE_1;
                    }
                    else
                    {
                        if (IsSignalFeatureEnabled("USER4"))
                        {
                            MstsSignalAspect = Aspect.Approach_1;
                            SignalAspect = SignalAspect.CH_IMAGE_2;
                        }
                        else
                        {
                            MstsSignalAspect = Aspect.Stop;
                            SignalAspect = SignalAspect.CH_IMAGE_H;
                        }
                    }
                }
            }

            SwissTCS(SignalAspect, thisDistantSignalInfo.Aspect);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}