namespace ORTS.Scripting.Script
{
    public class Info_1 : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.StopAndProceed;
            DirectionInfoAspect = DirectionInfoAspect.DIR1;

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}