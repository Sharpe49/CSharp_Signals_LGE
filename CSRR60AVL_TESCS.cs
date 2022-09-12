using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSRR60AVL_TESCS : FrSignalScript
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
                if (RouteSet)
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = FrSignalAspect.FR_C_BAL;
                }
                else
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    SignalAspect = FrSignalAspect.FR_S_BAL;
                }
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = FrSignalAspect.FR_VL_INF;
            }
            else
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    SignalAspect = FrSignalAspect.FR_RRCLI_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = FrSignalAspect.FR_RRCLI;
                }
            }

            FrenchTCS(true);

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}