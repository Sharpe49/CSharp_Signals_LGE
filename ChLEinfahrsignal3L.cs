using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLEinfahrsignal3L : SignalScript
    {
        public ChLEinfahrsignal3L()
        {
        }

        public override void Update()
        {
            List<string> thisDistantParts = TextSignalAspectToList(SignalId, "DISTANCE");
            List<string> thisInfoParts = TextSignalAspectToList(SignalId, "INFO");
            List<string> nextIdentifierParts = TextSignalAspectToList(NextSignalId("REPEATER"), "REPEATER");

            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "CH_IMAGE_H";
            }
            else
            {
                if (RouteSet)
                {
                    if (IsSignalFeatureEnabled("USER2"))
                    {
                        MstsSignalAspect = Aspect.Approach_1;
                        TextSignalAspect = "CH_IMAGE_2";
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        TextSignalAspect = "CH_IMAGE_1";
                    }
                }
                else
                {
                    if (thisInfoParts.Contains("CH_INFO_IMAGE_2")
                        || thisInfoParts.Contains("CH_INFO_IMAGE_3")
                        || thisInfoParts.Contains("CH_INFO_IMAGE_5")
                        || thisInfoParts.Contains("CH_INFO_IMAGE_6"))
                    {
                        MstsSignalAspect = Aspect.Approach_1;
                        TextSignalAspect = "CH_IMAGE_2";
                    }
                    else if (thisInfoParts.Contains("CH_INFO_IMAGE_1"))
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        TextSignalAspect = "CH_IMAGE_1";
                    }
                    else
                    {
                        if (IsSignalFeatureEnabled("USER4"))
                        {
                            MstsSignalAspect = Aspect.Approach_1;
                            TextSignalAspect = "CH_IMAGE_2";
                        }
                        else
                        {
                            MstsSignalAspect = Aspect.Stop;
                            TextSignalAspect = "CH_IMAGE_H";
                        }
                    }
                }
            }

            if (thisDistantParts.Count <= 0)
            {
                switch (TextSignalAspect)
                {
                    case "CH_IMAGE_H":
                        TextSignalAspect += " CH_CROCODILE_SF CH_KVB_CHAMP_S_C_BM CH_KVB_CHAMP_VRA_V40";
                        break;

                    case "CH_IMAGE_2":
                        TextSignalAspect += " CH_CROCODILE_SO CH_KVB_CHAMP_S_VL_INF CH_KVB_CHAMP_VRA_V40";
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