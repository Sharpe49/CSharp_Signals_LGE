using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSAR30VLVL : FrSignalScript
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
            else if (AnnounceByA(nextNormalParts, false, true))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = FrSignalAspect.FR_A;
            }
            else if (AnnounceByR(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = FrSignalAspect.FR_R;
            }
            else if (AnnounceByVLCLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = FrSignalAspect.FR_VLCLI_ANN;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = FrSignalAspect.FR_VL_SUP;
            }

            FrenchTCS();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}