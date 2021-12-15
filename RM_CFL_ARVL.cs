using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class RM_CFL_ARVL : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            List<string> thisRepeaterParts = TextSignalAspectToList(SignalId, "REPEATER");
            List<string> nextRepeaterParts = FindSignalAspect("LU_SFVo", "REPEATER", 5).Split(' ').ToList();

            if (thisNormalParts.Contains("LU_SFP1"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "";
            }
            else if (nextNormalParts.Contains("EOA")
                || nextNormalParts.Contains("LU_SFP1")
                || nextNormalParts.Contains("FR_TABLEAU_G_D"))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "LU_SFAv1";
            }
            else if (nextNormalParts.Contains("LU_SFP3")
                || thisRepeaterParts.Contains("LU_SFAvVo_PRESENTE")
                || nextRepeaterParts.Contains("LU_SFVo_PRESENTE"))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "LU_SFAv3";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "LU_SFAv2";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}