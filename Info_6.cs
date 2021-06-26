using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class Info_6 : CsSignalScript
    {
        public Info_6()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_1;
            TextSignalAspect = "DIR6";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}