namespace ORTS.Scripting.Script
{
    public class Info_3 : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Approach_1;
            DirectionInfoAspect = DirectionInfoAspect.DIR3;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}