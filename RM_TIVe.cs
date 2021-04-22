using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    // TIVR
    public class RM_TIVe : CsSignalScript
    {
        public RM_TIVe()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            string thisNormalSignalTextAspect = IdTextSignalAspect(SignalId, "NORMAL");
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : string.Empty;

            List<string> parts;
            if (thisNormalSignalTextAspect.Length > 0)
            {
                parts = thisNormalSignalTextAspect.Split(' ').ToList();
            }
            else
            {
                parts = nextNormalSignalTextAspect.Split(' ').ToList();
            }

            if (parts.Contains("FR_C"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TIVR_ETEINT";
            }
            else if (!RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TIVR_PRESENTE";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TIVR_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}