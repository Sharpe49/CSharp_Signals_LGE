namespace ORTS.Scripting.Script
{
    public class RM_CFL_IU : SignalScript
    {
        public RM_CFL_IU()
        {
        }

        public override void Update()
        {
            if (CurrentBlockState != BlockState.Clear
                && TrainHasCallOn())
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "LU_SFVo_PRESENTE";
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "LU_SFVo_EFFACE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}