using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class RM_CFL_TECS_AVL : SignalScript
    {
        public RM_CFL_TECS_AVL()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");

            if (thisNormalParts.Contains("LU_SFP1"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "LU_SFCCI_O_EFFACE";
            }
            else if (!RouteSet)
            {
                if (nextNormalParts.Contains("LU_SFP1"))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "LU_SFAv1 LU_SFCCI_O_EFFACE";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "LU_SFAv2 LU_SFCCI_O_EFFACE";
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "LU_SFAv2 LU_SFCCI_O_PRESENTE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}