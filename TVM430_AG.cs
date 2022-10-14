namespace ORTS.Scripting.Script
{
    public class TVM430_AG : FrSignalScript
    {
        TvmSpeedType VeAg = TvmSpeedType._320V;

        public override void Initialize()
        {
            if (HasHead(8))
            {
                VeAg = TvmSpeedType._000;
            }
            else if (HasHead(7))
            {
                VeAg = TvmSpeedType._60;
            }
            else if (HasHead(6))
            {
                VeAg = TvmSpeedType._80;
            }
            else if (HasHead(5))
            {
                VeAg = TvmSpeedType._130;
            }
            else if (HasHead(4))
            {
                VeAg = TvmSpeedType._160;
            }
            else if (HasHead(3))
            {
                VeAg = TvmSpeedType._170;
            }
            else if (HasHead(2))
            {
                VeAg = TvmSpeedType._200;
            }
            else if (HasHead(1))
            {
                VeAg = TvmSpeedType._220;
            }
            else
            {
                VeAg = TvmSpeedType._230;
            }
        }

        public override void Update()
        {
            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "FR_TVM430_AG Ve" + VeAg.ToString().Substring(1);
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}