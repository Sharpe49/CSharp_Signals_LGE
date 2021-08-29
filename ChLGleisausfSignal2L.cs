using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLGleisausfSignal2L : SignalScript
    {
        public ChLGleisausfSignal2L()
        {
        }

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