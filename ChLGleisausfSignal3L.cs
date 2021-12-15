using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLGleisausfSignal3L : ChSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
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
                    if (thisDistantParts.Count <= 0
                        && (nextNormalParts.Contains("CH_IMAGE_H")
                            || nextNormalParts.Contains("CH_IMAGE_6")))
                    {
                        if (nextIdentifierParts.Contains("CH_MARQUEUR_DE_SORTIE_DE_GARE"))
                        {
                            MstsSignalAspect = Aspect.Stop;
                            TextSignalAspect = "CH_IMAGE_H";
                        }
                        else
                        {
                            MstsSignalAspect = Aspect.Approach_1;
                            TextSignalAspect = "CH_IMAGE_2";
                        }
                    }
                    else if (IsSignalFeatureEnabled("USER2"))
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