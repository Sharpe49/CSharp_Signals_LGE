using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CvNain : SignalScript
    {
        public CvNain()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed
                || nextNormalParts.Contains("FR_FSO"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_CV";
            }
            else if (nextNormalParts.Contains("ESUBO")
                && (nextNormalParts.Contains("FR_C_BAL") || nextNormalParts.Contains("FR_CV")))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_CV";
            }
            else if (IsSignalFeatureEnabled("USER1") && CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_CV";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_M";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}