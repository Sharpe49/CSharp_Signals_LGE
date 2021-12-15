using Orts.Simulation.Signalling;
using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public abstract class FrSignalScript : SignalScript
    {
        public bool CommandAspectC(List<string> nextNormalParts) => !Enabled
                || CurrentBlockState == BlockState.Obstructed
                || nextNormalParts.Contains("FR_FSO")
                || HoldState == HoldState.StationStop
                || HoldState == HoldState.ManualLock;

        public bool CommandAspectS() => CurrentBlockState != BlockState.Clear
                || HoldState == HoldState.ManualLock;

        public bool AnnounceByA(List<string> aspects, bool announceRR = true, bool announceRRCLI = true)
        {
            if (HoldState == HoldState.ManualApproach)
            {
                return true;
            }

            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "EOA":
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
                        return true;

                    case "FR_RR_A":
                    case "FR_RR_ACLI":
                    case "FR_RR":
                        return announceRR;

                    case "FR_RRCLI_A":
                    case "FR_RRCLI_ACLI":
                    case "FR_RRCLI":
                        return announceRRCLI;
                }
            }

            return false;
        }

        public bool AnnounceByACLI(List<string> aspects)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "FR_A":
                    case "FR_R":
                        return true;
                }
            }

            return false;
        }

        public bool AnnounceByVLCLI(List<string> aspects)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "FR_A":
                    case "FR_R":
                    case "FR_ACLI":
                    case "FR_RCLI":
                    case "FR_RCLI_ACLI":
                        return true;
                }
            }

            return false;
        }

        public bool AnnounceByR(List<string> aspects, bool doubleAnnounce = false)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "FR_RR":
                    case "FR_RR_A":
                    case "FR_RR_ACLI":
                        return true;

                    case "FR_R":
                        return doubleAnnounce;
                }
            }

            return false;
        }

        public bool AnnounceByRCLI(List<string> aspects, bool doubleAnnounce = false)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "FR_RRCLI":
                    case "FR_RRCLI_A":
                    case "FR_RRCLI_ACLI":
                        return true;

                    case "FR_RCLI":
                        return doubleAnnounce;
                }
            }

            return false;
        }

        public bool AnnounceByRCLI_ACLI(List<string> aspects)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "FR_RRCLI_A":
                        return true;
                }
            }

            return false;
        }

        protected string FrenchTCS(string textAspect, bool signalHasSpeedRepeater = false, bool distantSignal = false)
        {
            return " " + FrenchCrocodile(textAspect) + " " + FrenchKVB(textAspect, signalHasSpeedRepeater, distantSignal);
        }

        protected string FrenchCrocodile(string textAspect)
        {
            switch (textAspect)
            {
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
                case "FR_D":
                case "FR_RR_A":
                case "FR_RR_ACLI":
                case "FR_RRCLI_A":
                case "FR_RRCLI_ACLI":
                case "FR_A":
                case "FR_R":
                case "FR_ACLI":
                case "FR_RCLI":
                case "FR_RCLI_ACLI":
                case "LU_SFP1":
                case "LU_SFAv1":
                case "LU_SFAv3":
                    return "CROCODILE_SF";

                default:
                    return "CROCODILE_SO";
            }
        }

        protected string FrenchKVB(string textAspect, bool signalHasSpeedRepeater = false, bool distantSignal = false)
        {
            List<string> kvbFields = new List<string>();

            // VAN
            switch (textAspect)
            {
                case "FR_R":
                    kvbFields.Add("KVB_VAN_V30");
                    break;

                case "FR_RCLI":
                case "FR_RCLI_ACLI":
                    kvbFields.Add("KVB_VAN_V60");
                    break;
            }

            if (distantSignal)
            {
                // S
                switch (textAspect)
                {
                    case "FR_D":
                    case "FR_A":
                    case "FR_ACLI":
                    case "FR_RCLI_ACLI":
                        kvbFields.Add("KVB_S_REOCS");
                        break;

                    default:
                        kvbFields.Add("KVB_S_REOVL");
                        break;
                }
            }
            else
            {
                // VRA
                switch (textAspect)
                {
                    case "FR_RR_A":
                    case "FR_RR_ACLI":
                    case "FR_RR":
                        kvbFields.Add("KVB_VRA_V30");
                        break;

                    case "FR_RRCLI_A":
                    case "FR_RRCLI_ACLI":
                    case "FR_RRCLI":
                        kvbFields.Add("KVB_VRA_V60");
                        break;

                    default:
                        if (signalHasSpeedRepeater)
                        {
                            kvbFields.Add("KVB_VRA_AA");
                        }
                        break;
                }

                switch (textAspect)
                {
                    case "FR_C_BAL":
                    case "FR_C_BAPR":
                    case "FR_CV":
                        kvbFields.Add("KVB_S_C_BAL");
                        break;

                    case "FR_C_BM":
                    case "FR_S_BM":
                        kvbFields.Add("KVB_S_S_BM");
                        break;

                    case "FR_S_BAL":
                    case "FR_S_BAPR":
                    case "FR_SCLI":
                    case "FR_MCLI":
                    case "FR_M":
                        kvbFields.Add("KVB_S_S_BAL");
                        break;

                    case "FR_RR_A":
                    case "FR_RRCLI_A":
                    case "FR_A":
                        kvbFields.Add("KVB_S_A");
                        break;

                    case "FR_RR_ACLI":
                    case "FR_RRCLI_ACLI":
                    case "FR_RCLI_ACLI":
                    case "FR_ACLI":
                        kvbFields.Add("KVB_S_ACLI");
                        break;

                    case "FR_VLCLI_ANN":
                        kvbFields.Add("KVB_S_VLCLI");
                        break;

                    case "FR_VL_INF":
                        kvbFields.Add("KVB_S_VL_INF");
                        break;

                    case "FR_VL_SUP":
                        kvbFields.Add("KVB_S_VL_SUP");
                        break;
                }
            }

            return string.Join(" ", kvbFields);
        }
    }
}
