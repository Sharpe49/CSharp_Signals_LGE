using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class CSMAAR60VL : FrSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            string ipcsInformation = FindSignalAspect("FR_IPCS", "INFO", 3);

            if (CommandAspectC(nextNormalParts))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                if (ipcsInformation.Contains("FR_IPCS_ENTREE_CONTRE_SENS")
                    && !IsSignalFeatureEnabled("USER3"))
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
            else if (nextNormalParts.Contains("FR_TABLEAU_G_D"))
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = FrSignalAspect.FR_M;
            }
            else if (ipcsInformation.Contains("FR_IPCS_ENTREE_CONTRE_SENS"))
            {
                if (IsSignalFeatureEnabled("USER3")
                    && AnnounceByA(nextNormalParts, true, false))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = FrSignalAspect.FR_A;
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && AnnounceByRCLI(nextNormalParts, IsSignalFeatureEnabled("USER2")))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = FrSignalAspect.FR_RCLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = FrSignalAspect.FR_VL_INF;
                }
            }
            else
            {
                if (AnnounceByA(nextNormalParts, true, false))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = FrSignalAspect.FR_A;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByRCLI_ACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    SignalAspect = FrSignalAspect.FR_RCLI_ACLI;
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    SignalAspect = FrSignalAspect.FR_ACLI;
                }
                else if (AnnounceByRCLI(nextNormalParts, IsSignalFeatureEnabled("USER2")))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = FrSignalAspect.FR_RCLI;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = FrSignalAspect.FR_VL_INF;
                }
            }

            FrenchTCS();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}