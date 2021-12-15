using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CARRVL_CE : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            Aspect thisSignalInfoAspect = IdSignalAspect(SignalId, "INFO");
            Aspect nextSignalInfoAspect = IdSignalAspect(NextSignalId("INFO"), "INFO");

            if (CommandAspectC(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (thisSignalInfoAspect != nextSignalInfoAspect)
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "FR_RR_A";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_RR";
                }
            }
            else
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_VL_INF";
                }
            }

            TextSignalAspect += FrenchTCS(TextSignalAspect, true);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}