using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class KVB_FZ : CsSignalScript
    {
        public KVB_FZ()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_FZ";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}