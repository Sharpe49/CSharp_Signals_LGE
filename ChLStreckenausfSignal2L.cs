using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLStreckenausfSignal2L : SignalScript
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

            if (thisDistantParts.Count <= 0)
            {
                switch (TextSignalAspect)
                {
                    case "CH_IMAGE_H":
                        TextSignalAspect += " CROCODILE_SF KVB_S_S_BM";
                        break;

                    default:
                        TextSignalAspect += " CROCODILE_SO KVB_S_VL_INF";
                        break;
                }
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}