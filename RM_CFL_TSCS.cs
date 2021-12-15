using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_CFL_TSCS : FrSignalScript
    {
        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");

            if (thisNormalParts.Contains("LU_SFP1"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "LU_SFCCI_TF_EFFACE";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "LU_SFCCI_TF_PRESENTE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}