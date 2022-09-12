using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class Rappel_Ralentissement : FrSignalScript
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
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                SignalAspect = FrSignalAspect.FR_S_BAL;
            }
            else if (speedInformation.Contains("FR_VITESSE_AIGUILLE"))
            {
                if (speedInformation.Contains("FR_VITESSE_AIGUILLE_30"))
                {
                    if (AnnounceByA(nextNormalParts))
                    {
                        MstsSignalAspect = Aspect.Approach_2;
                        SignalAspect = FrSignalAspect.FR_RR_A;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Approach_3;
                        SignalAspect = FrSignalAspect.FR_RR;
                    }
                }
                else if (speedInformation.Contains("FR_VITESSE_AIGUILLE_60"))
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
                else
                {
                    if (AnnounceByA(nextNormalParts))
                    {
                        MstsSignalAspect = Aspect.Approach_1;
                        SignalAspect = FrSignalAspect.FR_A;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.Clear_1;
                        SignalAspect = FrSignalAspect.FR_VL_INF;
                    }
                }
            }
            else if (RouteSet)
            {
                if (AnnounceByA(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = FrSignalAspect.FR_A;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    SignalAspect = FrSignalAspect.FR_VL_INF;
                }
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