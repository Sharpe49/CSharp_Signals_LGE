using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class Info_0_7 : CsSignalScript
    {
        public Info_0_7()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            int direction = 0;
            direction += IsSignalFeatureEnabled("USER1") ? 1 : 0;
            direction += IsSignalFeatureEnabled("USER2") ? 2 : 0;
            direction += IsSignalFeatureEnabled("USER3") ? 3 : 0;
            direction += IsSignalFeatureEnabled("USER4") ? 4 : 0;

            MstsSignalAspect = (Aspect)direction;
            TextSignalAspect = "DIR" + direction.ToString();

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}