using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class BandeJaune : SignalScript
    {
        public BandeJaune()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            string infoSignal = FindSignalAspect("BJ_VOIE", "INFO", 5);

            string thisNormalSignalTextAspect = IdTextSignalAspect(SignalId, "NORMAL");
            List<string> thisNormalParts = thisNormalSignalTextAspect.Split(' ').ToList();

            if (!Enabled
                || thisNormalParts.Contains("FR_C_BAL")
                || thisNormalParts.Contains("FR_S_BAL")
                || thisNormalParts.Contains("FR_SCLI"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_BJ_EFFACEE";
            }
            else if (infoSignal.Contains("BJ_VOIE_OCCUPEE"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_BJ_PRESENTEE";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_BJ_EFFACEE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}