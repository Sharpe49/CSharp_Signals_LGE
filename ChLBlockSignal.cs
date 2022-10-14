namespace ORTS.Scripting.Script
{
    public class ChLBlockSignal : ChSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisIdentifierSignalInfo = DeserializeAspect(SignalId, "REPEATER");

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.CH_IMAGE_H;
                DrawState = 0;
            }
            else if (thisIdentifierSignalInfo.ChInfoAspect == ChInfoAspect.CH_SIGNAL_AVANCE)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.CH_IMAGE_1;
                DrawState = 5;
            }
            else if (AnnounceByImageW(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.CH_IMAGE_W;
                DrawState = 1;
            }
            else if (AnnounceByImage2(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.CH_IMAGE_2A;
                DrawState = 2;
            }
            else if (AnnounceByImage3(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.CH_IMAGE_3A;
                DrawState = 3;
            }
            else if (AnnounceByImage5(nextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.CH_IMAGE_5A;
                DrawState = 4;
            }
            else
            {
                if (thisIdentifierSignalInfo.ChInfoAspect == ChInfoAspect.CH_SIGNAL_COMBINE)
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.CH_IMAGE_1;
                    DrawState = 5;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.CH_IMAGE_1A;
                    DrawState = 6;
                }
            }

            SwissCombinedTCS(SignalAspect);

            SerializeAspect();
        }
    }
}