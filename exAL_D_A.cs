namespace ORTS.Scripting.Script
{
    /// <summary>
    /// Plaque d'identification
    /// </summary>
    public class exAL_D_A : FrSignalScript
    {
        public override void Update()
        {
            if (IsSignalFeatureEnabled("USER1"))
            {
                MstsSignalAspect = Aspect.Clear_1;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}