using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class Pancarte_G_D : CsSignalScript
    {
        public Pancarte_G_D()
        {

        }

        public override void Initialize()
        {

        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_3;
            TextSignalAspect = "FR_TABLEAU_G_D";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}