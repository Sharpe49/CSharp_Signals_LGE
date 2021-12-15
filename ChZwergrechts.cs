using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChZwergrechts : ChSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> nextShuntingParts = TextSignalAspectToList(NextSignalId("SHUNTING"), "SHUNTING");

            if (CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
            }
            else if (nextNormalParts.Contains("CH_IMAGE_H")
                || (nextShuntingParts.Contains("CH_NAIN_FERME") && !IsSignalFeatureEnabled("USER1")))
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