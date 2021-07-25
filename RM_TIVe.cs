using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    // TIVR
    public class RM_TIVe : SignalScript
    {
        public RM_TIVe()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");

            List<string> parts;
            if (thisNormalParts.Count > 0)
            {
                parts = thisNormalParts;
            }
            else
            {
                parts = nextNormalParts;
            }

            if (parts.Contains("FR_C_BAL"))
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