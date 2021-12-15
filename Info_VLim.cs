namespace ORTS.Scripting.Script
{
    public class Info_VLim : FrSignalScript
    {
        public override void Update()
        {
            if (HasHead(1))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_30";
            }
            else if (HasHead(2))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_40";
            }
            else if (HasHead(3))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_50";
            }
            else if (HasHead(4))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_60";
            }
            else if (HasHead(5))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_70";
            }
            else if (HasHead(6))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_80";
            }
            else if (HasHead(7))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_90";
            }
            else if (HasHead(8))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_100";
            }
            else if (HasHead(9))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_110";
            }
            else if (HasHead(10))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_120";
            }
            else if (HasHead(11))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_130";
            }
            else if (HasHead(12))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_140";
            }
            else if (HasHead(13))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_150";
            }
            else if (HasHead(14))
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_160";
            }
            else
            {
                TextSignalAspect = "FR_VITESSE_AIGUILLE_NON_PARAMETREE";
            }

            MstsSignalAspect = Aspect.Clear_2;
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}