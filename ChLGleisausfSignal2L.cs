using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLGleisausfSignal2L : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisDistantParts = TextSignalAspectToList(SignalId, "DISTANCE");
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
                    if (nextNormalParts.Contains("CH_IMAGE_H"))
                    {
                        if (nextIdentifierParts.Contains("CH_MARQUEUR_DE_SORTIE_DE_GARE"))
                        {
                            MstsSignalAspect = Aspect.Stop;
                            TextSignalAspect = "CH_IMAGE_H";
                        }
                        else
                        {
                            MstsSignalAspect = Aspect.Clear_2;
                            TextSignalAspect = "CH_IMAGE_1";
                        }
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        TextSignalAspect = "CH_IMAGE_1";
                    }
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "CH_IMAGE_H";
                }
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