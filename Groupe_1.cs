namespace ORTS.Scripting.Script
{
    public class Groupe_1 : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Restricting;
            DirectionInfoAspect = DirectionInfoAspect.GROUPE1;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}