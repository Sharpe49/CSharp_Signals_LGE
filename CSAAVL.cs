using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSAAVL : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisTIVRParts = TextSignalAspectToList(SignalId, "TIVR");

            if (CommandAspectC(nextNormalParts, IsSignalFeatureEnabled("USER2"), IsSignalFeatureEnabled("USER4")))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = FrSignalAspect.FR_S_BAL;
            }
            else if (AnnounceByA(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = FrSignalAspect.FR_A;
            }
            else if (IsSignalFeatureEnabled("USER1")
                && AnnounceByACLI(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = FrSignalAspect.FR_ACLI;
            }
            else if (IsSignalFeatureEnabled("USER3")
                && AnnounceByVLCLI(nextNormalParts)
                && !thisTIVRParts.Contains("FR_TIVR_PRESENTE"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = FrSignalAspect.FR_VLCLI_ANN;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                if (IsSignalFeatureEnabled("USER3")
                    && !thisTIVRParts.Contains("FR_TIVR_PRESENTE"))
                {
                    SignalAspect = FrSignalAspect.FR_VL_SUP;
                }
                else
                {
                    SignalAspect = FrSignalAspect.FR_VL_INF;
                }
            }

            FrenchTCS();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}