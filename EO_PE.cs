using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    // TLD
    // EO - PE
    // L1 - L4
    // L1 - LGV
    public class EO_PE : SignalScript
    {
        public EO_PE()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            string direction = FindSignalAspect("DIR", "INFO", 5);

            bool thisNormalSignalAspectC = IdTextSignalAspect(SignalId, "NORMAL")
                .Split(' ')
                .ToList()
                .Contains("FR_C_BAL");

            if (thisNormalSignalAspectC)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_ETEINT";
            }
            else if (direction.Contains("DIR0"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TLD_1";
            }
            else if (direction.Contains("DIR1"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TLD_2";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_ETEINT";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}