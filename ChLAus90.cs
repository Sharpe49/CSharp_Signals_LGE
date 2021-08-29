using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLAusf90 : SignalScript
    {
        public ChLAusf90()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisDistantParts = TextSignalAspectToList(SignalId, "DISTANCE");
            List<string> nextIdentifierParts = TextSignalAspectToList(NextSignalId("REPEATER"), "REPEATER");

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "";
            }
            else if (thisDistantParts.Count <= 0
                && (nextNormalParts.Contains("CH_IMAGE_H")
                    || nextNormalParts.Contains("CH_IMAGE_6")))
            {
                if (nextIdentifierParts.Contains("CH_MARQUEUR_DE_SORTIE_DE_GARE"))
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "";
                }
                else
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "CH_INFO_IMAGE_6";
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "CH_INFO_IMAGE_5";
            }
        }
    }
}