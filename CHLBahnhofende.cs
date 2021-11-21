using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CHLBahnhofende : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> nextIdentifierParts = TextSignalAspectToList(NextSignalId("REPEATER"), "REPEATER");

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "CH_IMAGE_H";
            }
            else if (nextIdentifierParts.Contains("CH_SIGNAL_AVANCE"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "CH_IMAGE_1";
            }
            else
            {
                if (nextNormalParts.Contains("CH_IMAGE_H")
                    || nextNormalParts.Contains("CH_IMAGE_6"))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "CH_IMAGE_1 CH_PROCHAIN_SIGNAL_FERME";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "CH_IMAGE_1";
                }
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}