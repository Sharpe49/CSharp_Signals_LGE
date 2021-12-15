using Orts.Simulation.Signalling;
using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public abstract class ChSignalScript : SignalScript
    {
        public bool AnnounceByImageW(List<string> aspects)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "EOA":
                    case "CH_IMAGE_H":
                    case "FR_C_BAL":
                    case "FR_C_BAPR":
                    case "FR_C_BM":
                    case "FR_CV":
                    case "FR_S_BAL":
                    case "FR_S_BAPR":
                    case "FR_S_BM":
                    case "FR_SCLI":
                    case "FR_MCLI":
                    case "FR_M":
                    case "FR_RR":
                    case "FR_RR_A":
                    case "FR_RR_ACLI":
                        return true;
                }
            }

            return false;
        }

        public bool AnnounceByImage2(List<string> aspects)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "CH_IMAGE_2":
                    case "CH_IMAGE_6":
                        return true;
                }
            }

            return false;
        }

        public bool AnnounceByImage3(List<string> aspects)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "CH_IMAGE_3":
                    case "FR_RRCLI":
                    case "FR_RRCLI_A":
                    case "FR_RRCLI_ACLI":
                        return true;
                }
            }

            return false;
        }

        public bool AnnounceByImage5(List<string> aspects, bool doubleAnnounce = false)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "CH_IMAGE_5":
                        return true;
                }
            }

            return false;
        }

        public string SwissTCS(string normalAspect, string distantAspect)
        {
            return " " + SwissCrocodile(normalAspect, distantAspect) + " " + SwissKVB(normalAspect, distantAspect);
        }

        public string SwissCombinedTCS(string aspect)
        {
            return SwissTCS(aspect, aspect);
        }

        public string SwissCrocodile(string normalAspect, string distantAspect)
        {
            switch (normalAspect)
            {
                case "CH_IMAGE_H":
                case "CH_IMAGE_6":
                    return "CROCODILE_SF";

                case "CH_IMAGE_2":
                    switch (distantAspect)
                    {
                        case "CH_IMAGE_W":
                            return "CROCODILE_SF";

                        default:
                            return "CROCODILE_SO";
                    }

                case "CH_IMAGE_3":
                    switch (distantAspect)
                    {
                        case "CH_IMAGE_W":
                        case "CH_IMAGE_2*":
                            return "CROCODILE_SF";

                        default:
                            return "CROCODILE_SO";
                    }

                case "CH_IMAGE_5":
                    switch (distantAspect)
                    {
                        case "CH_IMAGE_W":
                        case "CH_IMAGE_2*":
                        case "CH_IMAGE_3*":
                            return "CROCODILE_SF";

                        default:
                            return "CROCODILE_SO";
                    }

                default:
                    switch (distantAspect)
                    {
                        case "CH_IMAGE_W":
                        case "CH_IMAGE_2*":
                        case "CH_IMAGE_3*":
                        case "CH_IMAGE_5*":
                            return "CROCODILE_SF";

                        default:
                            return "CROCODILE_SO";
                    }
            }
        }

        public string SwissKVB(string normalAspect, string distantAspect)
        {
            if (normalAspect.Length > 0)
            {
                switch (normalAspect)
                {
                    case "CH_IMAGE_H":
                        return "KVB_S_S_BM KVB_TIVE_G_AA";

                    case "CH_IMAGE_6":
                        return "KVB_S_A KVB_TIVE_G_V40";

                    case "CH_IMAGE_2":
                        switch (distantAspect)
                        {
                            case "CH_IMAGE_W":
                                return "KVB_S_A KVB_TIVE_G_V40";

                            case "CH_IMAGE_2*":
                                return "KVB_S_VL_INF KVB_TIVE_G_V40 KVB_TIVD_G_V40";

                            case "CH_IMAGE_3*":
                                return "KVB_S_VL_INF KVB_TIVE_G_V40 KVB_TIVD_G_V60";

                            case "CH_IMAGE_5*":
                                return "KVB_S_VL_INF KVB_TIVE_G_V40 KVB_TIVD_G_V90";

                            default:
                                return "KVB_S_VL_INF KVB_TIVE_G_V40";
                        }

                    case "CH_IMAGE_3":
                        switch (distantAspect)
                        {
                            case "CH_IMAGE_W":
                                return "KVB_S_A KVB_TIVE_G_V60";

                            case "CH_IMAGE_2*":
                                return "KVB_S_VL_INF KVB_TIVE_G_V60 KVB_TIVD_G_V40";

                            case "CH_IMAGE_3*":
                                return "KVB_S_VL_INF KVB_TIVE_G_V60 KVB_TIVD_G_V60";

                            case "CH_IMAGE_5*":
                                return "KVB_S_VL_INF KVB_TIVE_G_V60 KVB_TIVD_G_V90";

                            default:
                                return "KVB_S_VL_INF KVB_TIVE_G_V60";
                        }

                    case "CH_IMAGE_5":
                        switch (distantAspect)
                        {
                            case "CH_IMAGE_W":
                                return "KVB_S_A KVB_TIVE_G_V90";

                            case "CH_IMAGE_2*":
                                return "KVB_S_VL_INF KVB_TIVE_G_V90 KVB_TIVD_G_V40";

                            case "CH_IMAGE_3*":
                                return "KVB_S_VL_INF KVB_TIVE_G_V90 KVB_TIVD_G_V60";

                            case "CH_IMAGE_5*":
                                return "KVB_S_VL_INF KVB_TIVE_G_V90 KVB_TIVD_G_V90";

                            default:
                                return "KVB_S_VL_INF KVB_TIVE_G_V90";
                        }

                    default:
                        switch (distantAspect)
                        {
                            case "CH_IMAGE_W":
                                return "KVB_S_A KVB_TIVE_G_AA";

                            case "CH_IMAGE_2*":
                                return "KVB_S_VL_INF KVB_TIVE_G_AA KVB_TIVD_G_V40";

                            case "CH_IMAGE_3*":
                                return "KVB_S_VL_INF KVB_TIVE_G_AA KVB_TIVD_G_V60";

                            case "CH_IMAGE_5*":
                                return "KVB_S_VL_INF KVB_TIVE_G_AA KVB_TIVD_G_V90";

                            default:
                                return "KVB_S_VL_INF KVB_TIVE_G_AA";
                        }
                }
            }
            else
            {
                switch (distantAspect)
                {
                    case "CH_IMAGE_W":
                        return "KVB_S_REOCS";

                    case "CH_IMAGE_2*":
                        return "KVB_S_REOVL KVB_TIVD_G_V40";

                    case "CH_IMAGE_3*":
                        return "KVB_S_REOVL KVB_TIVD_G_V60";

                    case "CH_IMAGE_5*":
                        return "KVB_S_REOVL KVB_TIVD_G_V90";

                    default:
                        return "KVB_S_REOVL";
                }
            }
        }
    }
}
