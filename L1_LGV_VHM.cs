using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    // TLD
    // L1 - LGV
    public class L1_LGV_VHM : FrSignalScript
    {
        public override void Update()
        {
            string direction = FindSignalAspect("FR_ID", "ID", 1);

            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");

            if (thisNormalParts.Contains("FR_C_BAL")
                || thisNormalParts.Contains("FR_S_BAL")
                || thisNormalParts.Contains("FR_SCLI"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TLD_EFFACE";
            }
            else if (direction.Contains("FR_ID_1_FEU"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TLD_1";
            }
            else if (direction.Contains("FR_ID_2_FEUX"))
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