namespace ORTS.Scripting.Script
{
    public class RM_CFL_IU : LuSignalScript
    {
        public override void Update()
        {
            if (CurrentBlockState != BlockState.Clear
                && TrainHasCallOn())
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.LU_SFVo_PRESENTE;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.LU_SFVo_EFFACE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}