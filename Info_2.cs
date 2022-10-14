namespace ORTS.Scripting.Script
{
    public class Info_2 : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Restricting;
            DirectionInfoAspect = DirectionInfoAspect.DIR2;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}