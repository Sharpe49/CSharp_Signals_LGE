namespace ORTS.Scripting.Script
{
    public class Info_3 : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_1;
            TextSignalAspect = "DIR3";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}