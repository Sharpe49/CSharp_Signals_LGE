namespace ORTS.Scripting.Script
{
    public class Info_4 : SignalScript
    {
        public Info_4()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_2;
            TextSignalAspect = "DIR4";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}