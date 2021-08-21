using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_CFL_TIVmobA : SignalScript
    {
        public RM_CFL_TIVmobA()
        {
        }

        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            List<string> nextSpeedParts = TextSignalAspectToList(NextSignalId("TIVR"), "TIVR");

            if (thisNormalParts.Contains("LU_SFP1"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "LU_SFAvI_EFFACE";
            }
            else if (nextSpeedParts.Contains("LU_SFI_PRESENTE"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "LU_SFAvI_PRESENTE";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "LU_SFAvI_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}