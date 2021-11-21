using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class Oeilleton : SignalScript
    {
        public override void Update()
        {
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");

            if (!Enabled
                || thisNormalParts.Contains("FR_C_BAL")
                || thisNormalParts.Contains("FR_C_BAPR")
                || thisNormalParts.Contains("FR_C_BM")
                || thisNormalParts.Contains("FR_CV"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_OEILLETON_ETEINT";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_OEILLETON_ALLUME";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}