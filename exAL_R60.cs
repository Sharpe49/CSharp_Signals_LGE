using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class exAL_R60 : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "";
            }
            else if (AnnounceByRCLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_RCLI";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}