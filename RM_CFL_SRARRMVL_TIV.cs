using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_CFL_SRARRMVL_TIV : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisSpeedParts = TextSignalAspectToList(SignalId, "TIVR");
            string direction = FindSignalAspect("DIR", "INFO", 5);

            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "LU_SFP1 LU_SFVb1";
            }
            else if (direction.Contains("DIR5"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "LU_SFP1 LU_SFVb2";
            }
            else if (nextNormalParts.Contains("FR_TABLEAU_G_D"))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "LU_SFP3 LU_SFVb2";
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "LU_SFP3 LU_SFVb2";
            }
            else if (thisSpeedParts.Contains("LU_SFI_PRESENTE"))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "LU_SFP3 LU_SFVb2";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "LU_SFP2 LU_SFVb2";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}