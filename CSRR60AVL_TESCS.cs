using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR60AVL_TESCS : SignalScript
    {
        public CSRR60AVL_TESCS()
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
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                if (RouteSet)
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_C_BAL";
                }
                else
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    TextSignalAspect = "FR_S_BAL";
                }
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VL_INF";
            }
            else
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_RRCLI_A";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_RRCLI";
                }
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}