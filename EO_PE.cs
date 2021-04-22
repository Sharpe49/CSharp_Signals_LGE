using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    // TLD
    // EO - PE
    // L1 - L4
    // L1 - LGV
    public class EO_PE : CsSignalScript
    {
        public EO_PE()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            int nextSignalId = NextSignalId("INFO");
            string nextInfoSignalTextAspect = nextSignalId >= 0 ? IdTextSignalAspect(nextSignalId, "INFO") : string.Empty;
            List<string> parts = nextInfoSignalTextAspect.Split(' ').ToList();

            bool thisNormalSignalAspectC = IdTextSignalAspect(SignalId, "NORMAL")
                .Split(' ')
                .ToList()
                .Contains("FR_C");

            if (thisNormalSignalAspectC)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_OFF";
            }
            else if (parts.Contains("DIR0"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TLD_1";
            }
            else if (parts.Contains("DIR1"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TLD_2";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_OFF";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}