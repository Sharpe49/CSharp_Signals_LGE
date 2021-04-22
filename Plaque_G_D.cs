using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class Plaque_G_D : CsSignalScript
    {
        public Plaque_G_D()
        {

        }

        public override void Initialize()
        {

        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : string.Empty;
            List<string> nextNormalParts = nextNormalSignalTextAspect.Split(' ').ToList();

            int nextInfoSignalId = NextSignalId("INFO");
            string nextInfoSignalTextAspect = nextInfoSignalId >= 0 ? IdTextSignalAspect(nextInfoSignalId, "INFO") : string.Empty;

            List<string> thisNormalParts = IdTextSignalAspect(SignalId, "NORMAL").Split(' ').ToList();

            if (!Enabled
                || thisNormalParts.Contains("FR_C")
                || !nextNormalParts.Contains("FR_TABLEAU_G_D")
                || nextInfoSignalTextAspect == "DIR7")
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = string.Empty;
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_TABLEAU_G_D";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}