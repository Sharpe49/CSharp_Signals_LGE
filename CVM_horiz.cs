using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CVM_horiz : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (!Enabled
                || CurrentBlockState != BlockState.Clear
                || nextNormalParts.Contains("FR_FSO"))
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
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = FrSignalAspect.FR_M;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}