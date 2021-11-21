using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLVorsignal : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            List<string> thisIdentifierParts = TextSignalAspectToList(SignalId, "REPEATER");

            if (thisNormalParts.Contains("CH_IMAGE_H"))
            {
                if (thisIdentifierParts.Contains("CH_SIGNAL_SECTION_DE_LIGNE"))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "";
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "CH_IMAGE_W";
                }
            }
            else if (thisNormalParts.Contains("CH_IMAGE_6"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "";
            }
            else
            {
                if (AnnounceByImageW(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "CH_IMAGE_W";
                }
                else if (AnnounceByImage2(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "CH_IMAGE_2*";
                }
                else if (AnnounceByImage3(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "CH_IMAGE_3*";
                }
                else if (AnnounceByImage5(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "CH_IMAGE_5*";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "CH_IMAGE_1*";
                }
            }

            if (thisNormalParts.Count > 0)
            {
                string image = thisNormalParts.Find(s => s.StartsWith("CH_IMAGE"));

                switch (image)
                {
                    case "CH_IMAGE_H":
                        TextSignalAspect += " CROCODILE_SF KVB_S_S_BM KVB_TIVE_G_AA";
                        break;

                    case "CH_IMAGE_6":
                        TextSignalAspect += " CROCODILE_SF KVB_S_A KVB_TIVE_G_V40";
                        break;

                    case "CH_IMAGE_2":
                        switch (TextSignalAspect)
                        {
                            case "CH_IMAGE_W":
                                TextSignalAspect += " CROCODILE_SF KVB_S_A KVB_TIVE_G_V40";
                                break;

                            default:
                                TextSignalAspect += " CROCODILE_SO KVB_S_VL_INF KVB_TIVE_G_V40";
                                break;
                        }
                        break;

                    case "CH_IMAGE_3":
                        switch (TextSignalAspect)
                        {
                            case "CH_IMAGE_W":
                                TextSignalAspect += " CROCODILE_SF KVB_S_A KVB_TIVE_G_V60";
                                break;

                            case "CH_IMAGE_2*":
                                TextSignalAspect += " CROCODILE_SF KVB_S_VL_INF KVB_TIVE_G_V60 KVB_TIVD_G_V40";
                                break;

                            default:
                                TextSignalAspect += " CROCODILE_SO KVB_S_VL_INF KVB_TIVE_G_V60";
                                break;
                        }
                        break;

                    case "CH_IMAGE_5":
                        switch (TextSignalAspect)
                        {
                            case "CH_IMAGE_W":
                                TextSignalAspect += " CROCODILE_SF KVB_S_A KVB_TIVE_G_V90";
                                break;

                            case "CH_IMAGE_2*":
                                TextSignalAspect += " CROCODILE_SF KVB_S_VL_INF KVB_TIVE_G_V90 KVB_TIVD_G_V40";
                                break;

                            case "CH_IMAGE_3*":
                                TextSignalAspect += " CROCODILE_SF KVB_S_VL_INF KVB_TIVE_G_V90 KVB_TIVD_G_V60";
                                break;

                            default:
                                TextSignalAspect += " CROCODILE_SO KVB_S_VL_INF KVB_TIVE_G_V90";
                                break;
                        }
                        break;

                    case "CH_IMAGE_1":
                        switch (TextSignalAspect)
                        {
                            case "CH_IMAGE_W":
                                TextSignalAspect += " CROCODILE_SF KVB_S_A KVB_TIVE_G_AA";
                                break;

                            case "CH_IMAGE_2*":
                                TextSignalAspect += " CROCODILE_SF KVB_S_VL_INF KVB_TIVE_G_AA KVB_TIVD_G_V40";
                                break;

                            case "CH_IMAGE_3*":
                                TextSignalAspect += " CROCODILE_SF KVB_S_VL_INF KVB_TIVE_G_AA KVB_TIVD_G_V60";
                                break;

                            case "CH_IMAGE_5*":
                                TextSignalAspect += " CROCODILE_SF KVB_S_VL_INF KVB_TIVE_G_AA KVB_TIVD_G_V90";
                                break;

                            default:
                                TextSignalAspect += " CROCODILE_SO KVB_S_VL_INF KVB_TIVE_G_AA";
                                break;
                        }
                        break;
                }
            }
            else
            {
                switch (TextSignalAspect)
                {
                    case "CH_IMAGE_W":
                        TextSignalAspect += " CROCODILE_SF KVB_S_REOCS";
                        break;

                    case "CH_IMAGE_2*":
                        TextSignalAspect += " CROCODILE_SF KVB_S_REOVL KVB_TIVD_G_V40";
                        break;

                    case "CH_IMAGE_3*":
                        TextSignalAspect += " CROCODILE_SF KVB_S_REOVL KVB_TIVD_G_V60";
                        break;

                    case "CH_IMAGE_5*":
                        TextSignalAspect += " CROCODILE_SF KVB_S_REOVL KVB_TIVD_G_V90";
                        break;

                    default:
                        TextSignalAspect += " CROCODILE_SO KVB_S_REOVL";
                        break;
                }
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}