using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class Rappel_Ralentissement : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            string speedInformation = FindSignalAspect("FR_VITESSE_AIGUILLE", "INFO", 5);

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
            else if (speedInformation.Contains("FR_VITESSE_AIGUILLE"))
            {
                if (speedInformation.Contains("FR_VITESSE_AIGUILLE_30"))
                {
                    if (AnnounceByA(nextNormalParts))
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        TextSignalAspect = "FR_RR_A";
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Approach_3;
                        TextSignalAspect = "FR_RR";
                    }
                }
                else if (speedInformation.Contains("FR_VITESSE_AIGUILLE_60"))
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
            }
            else if (RouteSet)
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

            TextSignalAspect += FrenchTCS(TextSignalAspect, true);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}