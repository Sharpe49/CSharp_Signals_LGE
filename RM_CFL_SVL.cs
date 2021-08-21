using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class RM_CFL_SVL : SignalScript
    {
        public RM_CFL_SVL()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            string direction = FindSignalAspect("DIR", "INFO", 5);

            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "LU_SFP1";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "LU_SFP2";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}