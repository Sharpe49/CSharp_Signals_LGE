using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class SSAVLVL : SignalScript
    {
        public SSAVLVL()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_SCLI";
            }
            else if (AnnounceByA(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else if (AnnounceByVLCLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_VLCLI_ANN";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_SUP";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}