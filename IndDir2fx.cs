using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class IndDir2fx : SignalScript
    {
        public IndDir2fx()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            string direction = FindSignalAspect("DIR", "INFO", 5);

            bool thisNormalSignalAspectC = IdTextSignalAspect(SignalId, "NORMAL")
                .Split(' ')
                .ToList()
                .Contains("FR_C_BAL");

            if (!Enabled || thisNormalSignalAspectC)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_ID_ETEINT";
            }
            else if (!RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_ID_1_FEU";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_ID_2_FEUX";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}