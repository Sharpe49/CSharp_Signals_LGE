using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CvNain : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CommandAspectC(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_CV;
            }
            else if (nextNormalParts.Contains("ESUBO")
                && (nextNormalParts.Contains("FR_C_BAL") || nextNormalParts.Contains("FR_CV")))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_CV;
            }
            else if (IsSignalFeatureEnabled("USER1") && CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_CV;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = FrSignalAspect.FR_M;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}