using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    // TLD
    // M - T3
    public class EO_M_T3 : CsSignalScript
    {
        public EO_M_T3()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            string thisNormalSignalTextAspect = IdTextSignalAspect(SignalId, "NORMAL");
            List<string> thisNormalSignalParts = thisNormalSignalTextAspect.Split(' ').ToList();

            if (thisNormalSignalParts.Contains("FR_C"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_OFF";
            }
            else if (thisNormalSignalParts.Contains("FR_RR")
                || thisNormalSignalParts.Contains("FR_RR_A")
                || thisNormalSignalParts.Contains("FR_RR_ACLI"))
            {
                if (RouteSet)
                {
                    MstsSignalAspect = Aspect.Clear_1;
                    TextSignalAspect = "FR_TLD_1";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "FR_TLD_2";
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_OFF";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}