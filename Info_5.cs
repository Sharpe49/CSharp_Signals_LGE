using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class Info_5 : CsSignalScript
    {
        public Info_5()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_3;
            TextSignalAspect = "DIR5";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}