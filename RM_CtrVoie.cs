using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class RM_CtrVoie : CsSignalScript
    {
        public RM_CtrVoie()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Stop;
            TextSignalAspect = "FR_FSO";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}