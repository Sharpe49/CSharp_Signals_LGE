namespace ORTS.Scripting.Script
{
    public class Info_6 : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Clear_1;
            DirectionInfoAspect = DirectionInfoAspect.DIR6;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}