using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class TVM430_Cab : FrSignalScript
    {
        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            Aspect nextNormalSignalMstsAspect = nextNormalSignalId >= 0 ? IdSignalAspect(nextNormalSignalId, "NORMAL") : Aspect.Stop;
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : "EOA";
            List<string> nextNormalParts = nextNormalSignalTextAspect.Split(' ').ToList();
            nextNormalSignalTextAspect = string.Join(" ", nextNormalParts.Where(x =>
                x.StartsWith("FR_TVM")
                || x.StartsWith("Ve")
                || x.StartsWith("Vc")
                || x.StartsWith("Va")));

            MstsSignalAspect = nextNormalSignalMstsAspect;
            TextSignalAspect = nextNormalSignalTextAspect + " BSP_ECS";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}