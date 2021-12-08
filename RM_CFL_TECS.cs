using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_CFL_TECS : SignalScript
    {
        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");

            if (thisNormalParts.Contains("LU_SFP1")
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "LU_SFCCI_O_EFFACE";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "LU_SFCCI_O_PRESENTE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}