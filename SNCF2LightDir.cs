using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class SNCF2LightDir : SignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (nextNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_ID_ETEINT";
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_ID_1_FEU";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_ID_2_FEUX";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}