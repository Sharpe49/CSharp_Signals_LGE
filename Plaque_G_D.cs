using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class Plaque_G_D : SignalScript
    {
        public Plaque_G_D()
        {
        }

        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            string direction = FindSignalAspect("DIR", "INFO", 5);

            if (!Enabled
                || thisNormalParts.Contains("FR_C_BAL")
                || !nextNormalParts.Contains("FR_TABLEAU_G_D")
                || direction.Contains("DIR7"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = string.Empty;
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "FR_TABLEAU_G_D";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}