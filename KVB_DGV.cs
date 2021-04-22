using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class KVB_DGV : CsSignalScript
    {
        public KVB_DGV()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_DGV";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}