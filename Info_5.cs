namespace ORTS.Scripting.Script
{
    public class Info_5 : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Approach_3;
            DirectionInfoAspect = DirectionInfoAspect.DIR5;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}