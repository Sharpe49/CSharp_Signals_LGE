using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    // TLD
    // EO - PE
    // L1 - L4
    // L1 - LGV
    public class EO_PE : SignalScript
    {
        public EO_PE()
        {
        }

        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            string direction = FindSignalAspect("DIR", "INFO", 5);

            if (thisNormalParts.Contains("FR_C_BAL"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_EFFACE";
            }
            else if (direction.Contains("DIR0"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TLD_1";
            }
            else if (direction.Contains("DIR1"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TLD_2";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}