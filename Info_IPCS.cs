namespace ORTS.Scripting.Script
{
    public class Info_IPCS : FrSignalScript
    {
        public override void Initialize()
        {
            if (IsSignalFeatureEnabled("USER1"))
            {
                MstsSignalAspect = Aspect.Clear_1;
                IpcsInfoAspect = IpcsInfoAspect.FR_IPCS_ENTREE_CONTRE_SENS;
            }
            else if (IsSignalFeatureEnabled("USER2"))
            {
                MstsSignalAspect = Aspect.Clear_2;
                IpcsInfoAspect = IpcsInfoAspect.FR_IPCS_SORTIE_CONTRE_SENS;
            }
            else
            {
                MstsSignalAspect = Aspect.Stop;
                IpcsInfoAspect = IpcsInfoAspect.FR_IPCS_NON_PARAMETRE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}