using Orts.Simulation.Signalling;
using System.Collections.Generic;
using System.Linq;

namespace ORTS.Scripting.Script
{
    public class RM_TSCS_Id2d : SignalScript
    {
        public RM_TSCS_Id2d()
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
                TextSignalAspect = "FR_TSCS_EFFACE";
            }
            else if (direction.Contains("DIR0") || direction.Contains("DIR1") || direction.Contains("DIR2"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "FR_TSCS_PRESENTE";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_TSCS_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}