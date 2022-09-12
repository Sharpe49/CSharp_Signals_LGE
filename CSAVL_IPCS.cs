using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSAVL_IPCS : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CurrentBlockState != BlockState.Clear
                || nextNormalParts.Contains("FR_FSO"))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = FrSignalAspect.FR_VL_INF;
            }

            FrenchTCS();

            SerializeAspect();
            DrawState = !Enabled ? GetDrawState("Off") : DefaultDrawState(MstsSignalAspect);
        }
    }
}