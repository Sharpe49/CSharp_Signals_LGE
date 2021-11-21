using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSAAVL : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisTIVRParts = TextSignalAspectToList(SignalId, "TIVR");

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
            else if (AnnounceByA(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else if (IsSignalFeatureEnabled("USER1")
                && AnnounceByACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                TextSignalAspect = "FR_ACLI";
            }
            else if (IsSignalFeatureEnabled("USER3")
                && AnnounceByVLCLI(nextNormalParts)
                && !thisTIVRParts.Contains("FR_TIVR_PRESENTE"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_VLCLI_ANN";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                if (IsSignalFeatureEnabled("USER3")
                    && !thisTIVRParts.Contains("FR_TIVR_PRESENTE"))
                {
                    TextSignalAspect = "FR_VL_SUP";
                }
                else
                {
                    TextSignalAspect = "FR_VL_INF";
                }
            }

            TextSignalAspect = AddTCS(TextSignalAspect);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}