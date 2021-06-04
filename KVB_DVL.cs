using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class KVB_DVL : CsSignalScript
    {
        public KVB_DVL()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "KVB_DVL";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}