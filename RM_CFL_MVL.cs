namespace ORTS.Scripting.Script
{
    public class RM_CFL_MVL : LuSignalScript
    {
        public override void Update()
        {
            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.LU_SFVb1;
            }
            else
            {
                MstsSignalAspect = Aspect.Approach_3;
                SignalAspect = SignalAspect.LU_SFVb2;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}