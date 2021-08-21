using Orts.Simulation.Signalling;
using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public abstract partial class SignalScript : CsSignalScript
    {
        public bool AnnounceByA(List<string> aspects, bool announceRR = true, bool announceRRCLI = true)
        {
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
    }
}
