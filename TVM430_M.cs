using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class TVM430_M : SignalScript
    {
        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "TVM430_FEU_BLANC_ETEINT";
            }
            else
            {
                MstsSignalAspect = Aspect.StopAndProceed;
                TextSignalAspect = "TVM430_FEU_BLANC_ALLUME";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}