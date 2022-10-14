namespace ORTS.Scripting.Script
{
    public class Info_VLim : FrSignalScript
    {
        public override void Update()
        {
            if (HasHead(1))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_30;
            }
            else if (HasHead(2))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_40;
            }
            else if (HasHead(3))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_50;
            }
            else if (HasHead(4))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_60;
            }
            else if (HasHead(5))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_70;
            }
            else if (HasHead(6))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_80;
            }
            else if (HasHead(7))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_90;
            }
            else if (HasHead(8))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_100;
            }
            else if (HasHead(9))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_110;
            }
            else if (HasHead(10))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_120;
            }
            else if (HasHead(11))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_130;
            }
            else if (HasHead(12))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_140;
            }
            else if (HasHead(13))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_150;
            }
            else if (HasHead(14))
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_160;
            }
            else
            {
                SpeedInfoAspect = SpeedInfoAspect.FR_VITESSE_AIGUILLE_NON_PARAMETREE;
            }

            MstsSignalAspect = Aspect.Clear_2;
            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}