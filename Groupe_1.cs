namespace ORTS.Scripting.Script
{
    public class Groupe_1 : FrSignalScript
    {
        public override void Update()
        {
            MstsSignalAspect = Aspect.Restricting;
            TextSignalAspect = "GROUPE1";

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}