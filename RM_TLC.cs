using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_TLC : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLC_DAMIER";
            }
            else if (nextNormalParts.Contains("FR_M")
                || nextNormalParts.Contains("FR_MCLI"))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_TLC_T";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TLC_SLD";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}