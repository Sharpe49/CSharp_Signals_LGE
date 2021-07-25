using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public abstract class SignalScript : CsSignalScript
    {
        public List<string> NextNormalSignalTextAspects
        {
            get
            {
                return TextSignalAspectToList(NextSignalIdWithoutTextAspect("FR_REPRISE_VL", "NORMAL"), "NORMAL");
            }
        }

        int InitialSignalNumClearAhead;
        public List<int> SignalsWithResume = new List<int>();

        public override void Initialize()
        {
            InitialSignalNumClearAhead = SignalNumClearAhead;
        }

        public string FindSignalAspect(string text, string signalType, int maxSignals)
        {
            string result = string.Empty;

            for (int i = 0; i < maxSignals; i++)
            {
                int signalId = NextSignalId(signalType, i);

                if (signalId >= 0)
                {
                    string aspect = IdTextSignalAspect(signalId, signalType);

                    if (aspect.Contains(text))
                    {
                        result = aspect;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        public int NextSignalIdWithoutTextAspect(string text, string sigfn, int count = 0)
        {
            int id = NextSignalId(sigfn, count);
            if (id < 0)
            {
                return -1;
            }
            else
            {
                string textAspect = IdTextSignalAspect(id, sigfn);

                if (textAspect.Contains(text))
                {
                    if (text == "FR_REPRISE_VL")
                    {
                        SignalsWithResume.Add(id);
                    }

                    return NextSignalIdWithoutTextAspect(text, sigfn, count + 1);
                }
                else
                {
                    return id;
                }
            }
        }

        public void UpdateSignalNumClearAhead()
        {
            int id = NextSignalId("NORMAL");

            if (id >= 0)
            {
                if (SignalsWithResume.Contains(id))
                {
                    SignalNumClearAhead = InitialSignalNumClearAhead + 1;
                }
                else
                {
                    SignalNumClearAhead = InitialSignalNumClearAhead;
                }
            }
        }

        public List<string> TextSignalAspectToList(int signalId, string signalType)
        {
            string textAspect = signalId >= 0
                ? IdTextSignalAspect(signalId, signalType)
                : (signalType == "NORMAL"
                    ? "EOA"
                    : string.Empty);

            return textAspect.Split(' ').ToList();
        }

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
