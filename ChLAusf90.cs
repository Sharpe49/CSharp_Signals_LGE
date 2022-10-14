namespace ORTS.Scripting.Script
{
    public class ChLAusf90 : ChSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisDistantSignalInfo = DeserializeAspect(SignalId, "DISTANCE");
            SignalInfo nextIdentifierSignalInfo = DeserializeAspect(NextSignalId("REPEATER"), "REPEATER");

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                InfoAspect = ChInfoAspect.None;
            }
            else if (thisDistantSignalInfo.Aspect != SignalAspect.None
                && (nextNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_H
                    || nextNormalSignalInfo.Aspect == SignalAspect.CH_IMAGE_6))
            {
                if (nextIdentifierSignalInfo.ChInfoAspect == ChInfoAspect.CH_MARQUEUR_DE_SORTIE_DE_GARE)
                {
                    MstsSignalAspect = Aspect.Stop;
                    InfoAspect = ChInfoAspect.None;
                }
                else
                {
                    MstsSignalAspect = Aspect.Restricting;
                    InfoAspect = ChInfoAspect.CH_INFO_IMAGE_6;
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_3;
                InfoAspect = ChInfoAspect.CH_INFO_IMAGE_5;
            }
        }
    }
}