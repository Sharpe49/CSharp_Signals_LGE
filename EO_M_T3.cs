using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    // TLD
    // M - T3
    public class EO_M_T3 : SignalScript
    {
        public override void Update()
        {
            List<string> thisNormalSignalParts = TextSignalAspectToList(SignalId, "NORMAL");

            if (thisNormalSignalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_ETEINT";
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
                TextSignalAspect = "FR_TLD_ETEINT";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}