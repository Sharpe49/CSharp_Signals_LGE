using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class TVM430_FinCab : CsSignalScript
    {
        public TVM430_FinCab()
        {

        }

        public override void Initialize()
        {

        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            Aspect nextNormalSignalMstsAspect = nextNormalSignalId >= 0 ? IdSignalAspect(nextNormalSignalId, "NORMAL") : Aspect.Stop;
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : string.Empty;

            MstsSignalAspect = nextNormalSignalMstsAspect;
            TextSignalAspect = nextNormalSignalTextAspect + " BSP_ESL";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}