using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class Info_3 : CsSignalScript
    {
        public Info_3()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_1;
            TextSignalAspect = "DIR3";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}