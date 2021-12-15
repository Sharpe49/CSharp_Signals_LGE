using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLStreckenausfSignal2L : ChSignalScript
    {
        public override void Update()
        {
            List<string> thisDistantParts = TextSignalAspectToList(SignalId, "DISTANCE");

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "CH_IMAGE_H";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "CH_IMAGE_1";
            }

            string distantAspect = string.Empty;

            if (thisDistantParts.Count > 0)
            {
                distantAspect = thisDistantParts.Find(s => s.StartsWith("CH_IMAGE"));
            }

            TextSignalAspect += SwissTCS(TextSignalAspect, distantAspect);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}