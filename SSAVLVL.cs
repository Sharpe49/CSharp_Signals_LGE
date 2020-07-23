using Orts.Formats.Msts;
using System.Collections.Generic;
using System.Linq;

namespace Orts.Simulation.Signalling
{
    public class SSAVLVL : CsSignalScript
    {
        public SSAVLVL()
        {

        }

        public override void Initialize()
        {

        }

        public override void Update()
        {
            List<string> nextSignalTextAspects = GetNextSignalTextAspects(MstsSignalFunction.NORMAL);

            if (BlockState != MstsBlockState.CLEAR)
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
                || x == "FR_RRCLI_A"
                || x == "FR_RRCLI_ACLI"
                || x == "FR_RRCLI"
                ).Count > 0)
            {
                MstsSignalAspect = MstsSignalAspect.APPROACH_1;
                TextSignalAspect = "FR_A";
            }
            else if (nextSignalTextAspects.FindAll(x => x == "FR_A"
                || x == "FR_R"
                || x == "FR_ACLI"
                || x == "FR_RCLI"
                || x == "FR_RCLI_ACLI"
                ).Count > 0)
            {
                MstsSignalAspect = MstsSignalAspect.APPROACH_3;
                TextSignalAspect = "FR_VLCLI_ANN";
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