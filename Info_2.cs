using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class Info_2 : CsSignalScript
    {
        public Info_2()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Restricting;
            TextSignalAspect = "DIR2";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}