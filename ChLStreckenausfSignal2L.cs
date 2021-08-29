using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLStreckenausfSignal2L : SignalScript
    {
        public ChLStreckenausfSignal2L()
        {
        }

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
                        TextSignalAspect += " CH_CROCODILE_SF CH_KVB_CHAMP_S_C_BM CH_KVB_CHAMP_VRA_V40";
                        break;

                    default:
                        TextSignalAspect += " CH_CROCODILE_SO CH_KVB_CHAMP_S_VL_INF";
                        break;
                }
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}