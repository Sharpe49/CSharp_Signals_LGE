using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class Info_1 : CsSignalScript
    {
        public Info_1()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.StopAndProceed;
            TextSignalAspect = "DIR1";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}