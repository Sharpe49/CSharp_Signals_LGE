namespace ORTS.Scripting.Script
{
    public class Info_IPCS : SignalScript
    {
        public override void Update()
        {
            if (IsSignalFeatureEnabled("USER1"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                TextSignalAspect = "FR_IPCS_ENTREE_CONTRE_SENS";
            }
            else if (IsSignalFeatureEnabled("USER2"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                TextSignalAspect = "FR_IPCS_SORTIE_CONTRE_SENS";
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                TextSignalAspect = "FR_IPCS_NON_PARAMETRE";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}