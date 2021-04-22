using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    // Tableau Baissez Panto mobile
    public class RM_BPmob : CsSignalScript
    {
        public RM_BPmob()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            string thisNormalSignalTextAspect = IdTextSignalAspect(SignalId, "NORMAL");
            List<string> thisNormalParts = thisNormalSignalTextAspect.Split(' ').ToList();

            int nextInfoSignalId = NextSignalId("INFO");
            string nextInfoSignalTextAspect = nextInfoSignalId >= 0 ? IdTextSignalAspect(nextInfoSignalId, "INFO") : string.Empty;
            List<string> nextInfoParts = nextInfoSignalTextAspect.Split(' ').ToList();

            if (!Enabled || thisNormalParts.Contains("FR_C"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_BP_ETEINT";
            }
            // Tableau BP présenté
            else if (IsSignalFeatureEnabled("USER2") && nextInfoParts.Contains("DIR1"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_BP_PRESENTE";
            }
            else if (IsSignalFeatureEnabled("USER3") && nextInfoParts.Contains("DIR2"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_BP_PRESENTE";
            }
            // Tableau BP effacé
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_BP_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}