using Orts.Simulation.Signalling;
using ORTS.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORTS.Scripting.Script
{
    public sealed class TextAspectBuilder
    {
        private readonly StringBuilder StringBuilder = new StringBuilder();

        public TextAspectBuilder Append(string value)
        {
            if (StringBuilder.Length > 0)
            {
                StringBuilder.Append(" ");
            }
            StringBuilder.Append(value);

            return this;
        }

        public TextAspectBuilder Append(string format, object arg0)
        {
            if (StringBuilder.Length > 0)
            {
                StringBuilder.Append(" ");
            }
            StringBuilder.AppendFormat(format, arg0);

            return this;
        }

        public TextAspectBuilder Clear()
        {
            StringBuilder.Clear();

            return this;
        }

        public override string ToString() => StringBuilder.ToString();
    }

    public abstract class SignalScript : CsSignalScript
    {
        public List<string> NextNormalSignalTextAspects
        {
            get
            {
                return TextSignalAspectToList(NextSignalIdWithoutTextAspect("FR_REPRISE_VL", "NORMAL"), "NORMAL");
            }
        }

        public List<int> SignalsWithResume = new List<int>();

        public override void Initialize()
        {
        }

        public override void Update()
        {
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

        public List<string> TextSignalAspectToList(int signalId, string signalType)
        {
            if (signalId < 0)
            {
                return (signalType == "NORMAL" ? new List<string>() { "EOA" } : new List<string>());
            }
            else
            {
                string textAspect = IdTextSignalAspect(signalId, signalType);
                List<string> list = new List<string>();
                int i = 0;

                while (textAspect.Length > 0 && i < 5)
                {
                    list = list.Concat(textAspect.Split(' ')).ToList();

                    i++;
                    textAspect = IdTextSignalAspect(signalId, signalType, i);
                }

                return list.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
            }
        }

        public override void HandleEvent(SignalEvent evt, string message = "")
        {
            Update();
        }

        public void SetSpeedLimitKpH(float passengerSpeedLimitKpH, float freightSpeedLimitKpH, bool asap, bool reset, bool noSpeedReduction, bool isWarning)
        {
            SetSpeedLimit(MpS.FromKpH(passengerSpeedLimitKpH), MpS.FromKpH(freightSpeedLimitKpH), asap, reset, noSpeedReduction, isWarning);
        }
    }
}
