using Orts.Formats.Msts;
using System.Collections.Generic;
using System.Linq;

namespace Orts.Simulation.Signalling
{
    public class CSAAR60VL : CsSignalScript
    {
        public bool AspectR = false;
        public bool AspectRCLI = false;

        public CSAAR60VL()
        {

        }

        public override void Initialize()
        {

        }

        public override void Update()
        {
            List<string> nextSignalTextAspects = GetNextSignalTextAspects(MstsSignalFunction.NORMAL);

            if (!Enabled
                || BlockState == MstsBlockState.JN_OBSTRUCTED)
            {
                MstsSignalAspect = MstsSignalAspect.STOP;
                TextSignalAspect = "FR_C";
            }
            else if (BlockState == MstsBlockState.OCCUPIED)
            {
                MstsSignalAspect = MstsSignalAspect.STOP_AND_PROCEED;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (nextSignalTextAspects.FindAll(x => x == "FR_C"
                || x == "FR_S_BAL"
                || x == "FR_S_BAPR"
                || x == "FR_S_BM"
                || x == "FR_SCLI"
                || x == "FR_RR_A"
                || x == "FR_RR_ACLI"
                || x == "FR_RR"
                ).Count > 0)
            {
                MstsSignalAspect = MstsSignalAspect.APPROACH_1;
                TextSignalAspect = "FR_A";
            }
            else if (nextSignalTextAspects.FindAll(x => x == "FR_A"
                || x == "FR_R"
                ).Count > 0)
            {
                MstsSignalAspect = MstsSignalAspect.APPROACH_2;
                TextSignalAspect = "FR_ACLI";
            }
            else if (nextSignalTextAspects.FindAll(x => x == "FR_RRCLI"
                || x == "FR_RRCLI_ACLI"
                ).Count > 0)
            {
                MstsSignalAspect = MstsSignalAspect.CLEAR_2;
                TextSignalAspect = "FR_RCLI";
            }
            else if (nextSignalTextAspects.FindAll(x => x == "FR_RRCLI_A").Count > 0)
            {
                MstsSignalAspect = MstsSignalAspect.RESTRICTING;
                TextSignalAspect = "FR_RCLI_ACLI";
            }
            else
            {
                MstsSignalAspect = MstsSignalAspect.CLEAR_1;
                TextSignalAspect = "FR_VL";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}