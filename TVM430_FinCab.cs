using System;
using System.Collections.Generic;
using System.Linq;
using static ORTS.Scripting.Script.TVM430Common;

namespace ORTS.Scripting.Script
{
    public class TVM430_FinCab : FrSignalScript
    {
        Dictionary<TvmSpeedType, Aspect> MstsTranslation = SNCFV320MstsTranslation;

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : "FR_TVM430 Ve80 Vc000";
            List<string> nextNormalParts = nextNormalSignalTextAspect.Split(' ').ToList();

            nextNormalSignalTextAspect = string.Join(" ", nextNormalParts.Where(x =>
                x.StartsWith("FR_TVM")
                || x.StartsWith("Ve")
                || x.StartsWith("Vc")
                || x.StartsWith("Va")));

            TvmSpeedType Vc = TvmSpeedType.Any;
            foreach (string part in nextNormalParts)
            {
                if (part.StartsWith("Vc"))
                {
                    Vc = (TvmSpeedType)Enum.Parse(typeof(TvmSpeedType), "_" + part.Substring(2));
                }
            }

            MstsSignalAspect = MstsTranslation[Vc];

            if (MstsSignalAspect < Aspect.Restricting)
            {
                MstsSignalAspect = Aspect.Restricting;
            }

            TextSignalAspect = nextNormalSignalTextAspect + " BSP_ESL";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}