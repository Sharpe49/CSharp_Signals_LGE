using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR60AVL_TSCS : SignalScript
    {
        public CSRR60AVL_TSCS()
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
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts))
                {
                    if (IsSignalFeatureEnabled("USER1"))
                    {
                        MstsSignalAspect = Aspect.Approach_3;
                        TextSignalAspect = "FR_RR_A";
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        TextSignalAspect = "FR_RRCLI_A";
                    }
                }
                else
                {
                    if (IsSignalFeatureEnabled("USER1"))
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        TextSignalAspect = "FR_RR";
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        TextSignalAspect = "FR_RRCLI";
                    }
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}