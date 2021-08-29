using Orts.Simulation.Signalling;
using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public abstract partial class SignalScript : CsSignalScript
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
    }
}
