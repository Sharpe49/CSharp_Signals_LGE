using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_CFL_TIVmobE : FrSignalScript
    {
        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");

            if (thisNormalParts.Contains("LU_SFP1"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "LU_SFI_EFFACE";
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "LU_SFI_PRESENTE";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "LU_SFI_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}