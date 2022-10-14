namespace ORTS.Scripting.Script
{
    public class BJ_Info_7 : FrSignalScript
    {
        public override void Update()
        {
            if (Enabled && CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Clear_2;
                DirectionInfoAspect = DirectionInfoAspect.BJ_VOIE_OCCUPEE;
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                DirectionInfoAspect = DirectionInfoAspect.BJ_VOIE_LIBRE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}