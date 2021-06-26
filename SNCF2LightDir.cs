using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class SNCF2LightDir : SignalScript
    {
        public SNCF2LightDir()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : "EOA";
            List<string> nextNormalParts = nextNormalSignalTextAspect.Split(' ').ToList();

            if (nextNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_ID_ETEINT";
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_ID_1_FEU";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_ID_2_FEUX";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}