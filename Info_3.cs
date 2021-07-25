namespace ORTS.Scripting.Script
{
    public class Info_3 : SignalScript
    {
        public Info_3()
        {
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_1;
            TextSignalAspect = "DIR3";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}