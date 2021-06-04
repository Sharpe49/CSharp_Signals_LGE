using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public class CSA_HSL : CsSignalScript
    {
        public CSA_HSL()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState == BlockState.Obstructed)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CurrentBlockState == BlockState.Occupied)
            {
                if (TrainHasCallOn())
                {
                    if (IsSignalFeatureEnabled("USER1"))
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        TextSignalAspect = "FR_SCLI";
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.StopAndProceed;
                        TextSignalAspect = "FR_S_BAL";
                    }
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "FR_C_BAL";
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_1;
                TextSignalAspect = "FR_A";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}