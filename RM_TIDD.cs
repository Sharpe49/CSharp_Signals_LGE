using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class RM_TIDD : SignalScript
    {
        public RM_TIDD()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            string direction = FindSignalAspect("FR_ID", "REPEATER", 5);

            List<string> thisNormalSignalAspect = IdTextSignalAspect(SignalId, "NORMAL")
                .Split(' ')
                .ToList();

            if (thisNormalSignalAspect.Contains("FR_C_BAL")
                || thisNormalSignalAspect.Contains("FR_S_BAL")
                || thisNormalSignalAspect.Contains("FR_SCLI"))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TIDD_ETEINT";
            }
            else if (direction.Contains("FR_ID_1_FEU"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_TIDD_GAUCHE";
            }
            else if (direction.Contains("FR_ID_2_FEUX"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_TIDD_DROITE";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TIDD_ETEINT";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}