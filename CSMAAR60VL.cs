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
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CommandAspectS())
            {
                if (ipcsInformation.Contains("FR_IPCS_ENTREE_CONTRE_SENS")
                    && !IsSignalFeatureEnabled("USER3"))
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
            else if (nextNormalParts.Contains("FR_TABLEAU_G_D"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_M";
            }
            else if (ipcsInformation.Contains("FR_IPCS_ENTREE_CONTRE_SENS"))
            {
                if (IsSignalFeatureEnabled("USER3")
                    && AnnounceByA(nextNormalParts, true, false))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else if (IsSignalFeatureEnabled("USER3")
                    && AnnounceByRCLI(nextNormalParts, IsSignalFeatureEnabled("USER2")))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_RCLI";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_VL_INF";
                }
            }
            else
            {
                if (AnnounceByA(nextNormalParts, true, false))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "FR_A";
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByRCLI_ACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "FR_RCLI_ACLI";
                }
                else if (IsSignalFeatureEnabled("USER1")
                    && AnnounceByACLI(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "FR_ACLI";
                }
                else if (AnnounceByRCLI(nextNormalParts, IsSignalFeatureEnabled("USER2")))
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_RCLI";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_VL_INF";
                }
            }

            TextSignalAspect += FrenchTCS(TextSignalAspect);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}