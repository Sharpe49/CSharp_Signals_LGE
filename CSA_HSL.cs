namespace ORTS.Scripting.Script
{
    public class CSA_HSL : FrSignalScript
    {
        public override void Update()
        {
            if (CommandAspectC(NextNormalSignalTextAspects))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = FrSignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                if (TrainHasCallOn())
                {
                    if (IsSignalFeatureEnabled("USER1"))
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        SignalAspect = FrSignalAspect.FR_SCLI;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.StopAndProceed;
                        SignalAspect = FrSignalAspect.FR_S_BAL;
                    }
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = FrSignalAspect.FR_C_BAL;
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = FrSignalAspect.FR_A;
            }

            FrenchTCS();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}