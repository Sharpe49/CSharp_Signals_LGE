using Orts.Formats.Msts;
using System.Collections.Generic;
using System.Linq;

namespace Orts.Simulation.Signalling
{
    public class BAPR_SVL : CsSignalScript
    {
        public BAPR_SVL()
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
                TextSignalAspect = "FR_S_BAPR";
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