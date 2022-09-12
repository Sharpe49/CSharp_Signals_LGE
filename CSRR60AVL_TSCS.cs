using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR60AVL_TSCS : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CommandAspectC(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = FrSignalAspect.FR_S_BAL;
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts))
                {
                    if (IsSignalFeatureEnabled("USER1"))
                    {
                        MstsSignalAspect = Aspect.Approach_3;
                        SignalAspect = FrSignalAspect.FR_RR_A;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        SignalAspect = FrSignalAspect.FR_RRCLI_A;
                    }
                }
                else
                {
                    if (IsSignalFeatureEnabled("USER1"))
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        SignalAspect = FrSignalAspect.FR_RR;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_2;
                        SignalAspect = FrSignalAspect.FR_RRCLI;
                    }
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }

            FrenchTCS(true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}