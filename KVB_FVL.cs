using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class KVB_FVL : CsSignalScript
    {
        public KVB_FVL()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_FVL";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}