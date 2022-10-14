namespace ORTS.Scripting.Script
{
    public class CSA_HSL : FrSignalScript
    {
        public override void Update()
        {
            if (CommandAspectC(NextNormalSignalInfo))
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = Script.SignalAspect.FR_C_BAL;
            }
            else if (CommandAspectS())
            {
                if (TrainHasCallOn())
                {
                    if (IsSignalFeatureEnabled("USER1"))
                    {
                        MstsSignalAspect = Aspect.Restricting;
                        SignalAspect = Script.SignalAspect.FR_SCLI;
                    }
                    else
                    {
                        MstsSignalAspect = Aspect.StopAndProceed;
                        SignalAspect = Script.SignalAspect.FR_S_BAL;
                    }
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    SignalAspect = Script.SignalAspect.FR_C_BAL;
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = Script.SignalAspect.FR_A;
            }

            FrenchTcs();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}