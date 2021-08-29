using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChZwergAbfrage : SignalScript
    {
        public ChZwergAbfrage()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;

            if (CurrentBlockState != BlockState.Clear
                || !RouteSet)
            {
                MstsSignalAspect = Aspect.Stop;
            }
            else if (nextNormalParts.Contains("CH_IMAGE_H"))
            {
                MstsSignalAspect = Aspect.Stop;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
            }

            TextSignalAspect = "";
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}