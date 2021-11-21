using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChAbfahrbefehl : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (nextNormalParts.Contains("CH_IMAGE_H"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "CH_AUTORISATION_DE_DEPART_EFFACEE";
            }
            else if (IsSignalFeatureEnabled("USER1"))
            {
                if (CurrentBlockState == BlockState.Clear)
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "CH_AUTORISATION_DE_DEPART_EFFACEE";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "CH_AUTORISATION_DE_DEPART_PRESENTEE";
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "CH_AUTORISATION_DE_DEPART_PRESENTEE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}