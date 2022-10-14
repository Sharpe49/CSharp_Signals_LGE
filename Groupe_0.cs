namespace ORTS.Scripting.Script
{
    public class Groupe_0 : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.StopAndProceed;
            DirectionInfoAspect = DirectionInfoAspect.GROUPE0;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}