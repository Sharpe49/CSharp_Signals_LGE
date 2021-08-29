using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLBlockSignal : SignalScript
    {
        public ChLBlockSignal()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> nextIdentifierParts = TextSignalAspectToList(SignalId, "REPEATER");

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "CH_IMAGE_H";
                DrawState = 0;
            }
            else if (nextIdentifierParts.Contains("CH_SIGNAL_AVANCE"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "CH_IMAGE_1";
                DrawState = 5;
            }
            else if (AnnounceByImageW(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "CH_IMAGE_W";
                DrawState = 1;
            }
            else if (AnnounceByImage2(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "CH_IMAGE_2*";
                DrawState = 2;
            }
            else if (AnnounceByImage3(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "CH_IMAGE_3*";
                DrawState = 3;
            }
            else if (AnnounceByImage5(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "CH_IMAGE_5*";
                DrawState = 4;
            }
            else
            {
                if (nextIdentifierParts.Contains("CH_SIGNAL_COMBINE"))
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "CH_IMAGE_1";
                    DrawState = 5;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "CH_IMAGE_1*";
                    DrawState = 6;
                }
            }

            switch (TextSignalAspect)
            {
                case "CH_IMAGE_H":
                    TextSignalAspect += " CH_CROCODILE_SF CH_KVB_CHAMP_S_C_BM CH_KVB_CHAMP_VRA_V40";
                    break;

                case "CH_IMAGE_W":
                    TextSignalAspect += " CH_CROCODILE_SF CH_KVB_CHAMP_S_A";
                    break;

                case "CH_IMAGE_2*":
                    TextSignalAspect += " CH_CROCODILE_SF CH_KVB_CHAMP_S_VL_INF CH_KVB_CHAMP_VAN_V40";
                    break;

                case "CH_IMAGE_3*":
                    TextSignalAspect += " CH_CROCODILE_SF CH_KVB_CHAMP_S_VL_INF CH_KVB_CHAMP_VAN_V60";
                    break;

                case "CH_IMAGE_5*":
                    TextSignalAspect += " CH_CROCODILE_SF CH_KVB_CHAMP_S_VL_INF CH_KVB_CHAMP_VAN_V90";
                    break;

                default:
                    TextSignalAspect += " CH_CROCODILE_SO CH_KVB_CHAMP_S_VL_INF";
                    break;
            }
        }
    }
}