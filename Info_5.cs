namespace ORTS.Scripting.Script
{
    public class Info_5 : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Approach_3;
            TextSignalAspect = "DIR5";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}