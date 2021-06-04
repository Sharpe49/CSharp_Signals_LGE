using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public abstract class SignalScript : CsSignalScript
    {
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
    }
}
