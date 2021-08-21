namespace ORTS.Scripting.Script
{
    public class RM_CFL_MVL : SignalScript
    {
        public RM_CFL_MVL()
        {
        }

        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "LU_SFVb1";
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_3;
                TextSignalAspect = "LU_SFVb2";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}