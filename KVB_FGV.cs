using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class KVB_FGV : CsSignalScript
    {
        public KVB_FGV()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_FGV";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}