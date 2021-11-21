using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class exAL_CvM : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CommandAspectC(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_CV";
            }
            else if (CommandAspectS())
            {
                if (IsSignalFeatureEnabled("USER2"))
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_CV";
                }
                else if (IsSignalFeatureEnabled("USER1"))
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    TextSignalAspect = "FR_MCLI";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_M";
                }
            }
            else
            {
                if (nextNormalParts.Contains("ESUBO")
                    && (nextNormalParts.Contains("FR_C_BAL") || nextNormalParts.Contains("FR_CV")))
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_CV";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_M";
                }
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}