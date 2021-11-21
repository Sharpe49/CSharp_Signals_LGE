using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLEinfahrsignal3L : SignalScript
    {
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
                        TextSignalAspect += " CROCODILE_SF KVB_S_S_BM KVB_TIVE_G_AA";
                        break;

                    case "CH_IMAGE_2":
                        TextSignalAspect += " CROCODILE_SO KVB_S_VL_INF KVB_TIVE_G_V40";
                        break;

                    default:
                        TextSignalAspect += " CROCODILE_SO KVB_S_VL_INF KVB_TIVE_G_AA";
                        break;
                }
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}