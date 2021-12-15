namespace ORTS.Scripting.Script
{
    public class CSA_HSL : FrSignalScript
    {
        public override void Update()
        {
            if (CommandAspectC(NextNormalSignalTextAspects))
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_C_BAL";
            }
            else if (CommandAspectS())
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

            TextSignalAspect += FrenchTCS(TextSignalAspect);

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}