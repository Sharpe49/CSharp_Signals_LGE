namespace ORTS.Scripting.Script
{
    public class ChLVorsignal : ChSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo thisIdentifierSignalInfo = DeserializeAspect(SignalId, "REPEATER");

            if (thisNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_H)
            {
                if (thisIdentifierSignalInfo.ChInfoAspect == ChInfoAspect.CH_SIGNAL_SECTION_DE_LIGNE)
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = SignalAspect.None;
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.CH_IMAGE_W;
                }
            }
            else if (thisNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_6)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = SignalAspect.None;
            }
            else
            {
                if (AnnounceByImageW(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = SignalAspect.CH_IMAGE_W;
                }
                else if (AnnounceByImage2(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.CH_IMAGE_2A;
                }
                else if (AnnounceByImage3(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = SignalAspect.CH_IMAGE_3A;
                }
                else if (AnnounceByImage5(nextNormalSignalInfo))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = SignalAspect.CH_IMAGE_5A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.CH_IMAGE_1A;
                }
            }

            if (thisNormalSignalInfo.Aspect == SignalAspect.None)
            {
                SwissTCS(SignalAspect.None, SignalAspect);
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}