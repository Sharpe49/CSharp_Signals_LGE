namespace ORTS.Scripting.Script
{
    public class BJ_Info_7 : SignalScript
    {
        public override void Update()
        {
            if (Enabled && CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "BJ_VOIE_OCCUPEE";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "BJ_VOIE_LIBRE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}