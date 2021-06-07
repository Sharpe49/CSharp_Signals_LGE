using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class RM_TECS_Id2d : SignalScript
    {
        public RM_TECS_Id2d()
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
                TextSignalAspect = "FR_TECS_EFFACE";
            }
            else if (direction.Contains("DIR3") || direction.Contains("DIR4"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_TECS_PRESENTE";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TECS_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}