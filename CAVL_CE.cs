using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class CAVL_CE : CsSignalScript
    {
        public CAVL_CE()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            int nextNormalSignalId = NextSignalId("NORMAL");
            string nextNormalSignalTextAspect = nextNormalSignalId >= 0 ? IdTextSignalAspect(nextNormalSignalId, "NORMAL") : "EOA";
            List<string> nextNormalParts = nextNormalSignalTextAspect.Split(' ').ToList();

            bool nextSignalSubo = nextNormalParts.Contains("ESUBO");

            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C";
            }
            else if (nextSignalSubo && nextNormalParts.Contains("FR_C"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C";
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                if (nextSignalSubo)
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_C";
                }
                else
                {
                    MstsSignalAspect = Aspect.StopAndProceed;
                    TextSignalAspect = "FR_S_BAL";
                }
            }
            else if (nextNormalParts.FindAll(x => x == "EOA"
                || x == "FR_C"
                || x == "FR_CV"
                || x == "FR_S_BAL"
                || x == "FR_S_BAPR"
                || x == "FR_S_BM"
                || x == "FR_SCLI"
                || x == "FR_MCLI"
                || x == "FR_M"
                || x == "FR_RR_A"
                || x == "FR_RR_ACLI"
                || x == "FR_RR"
                || x == "FR_RRCLI_A"
                || x == "FR_RRCLI_ACLI"
                || x == "FR_RRCLI"
                ).Count > 0)
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_VL_INF";
            }

            TextSignalAspect += " ESUBO";
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}