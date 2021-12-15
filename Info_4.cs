namespace ORTS.Scripting.Script
{
    public class Info_4 : FrSignalScript
    {
        public override void Initialize()
        {
            MstsSignalAspect = Aspect.Approach_2;
            TextSignalAspect = "DIR4";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}