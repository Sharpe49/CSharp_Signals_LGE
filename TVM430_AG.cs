namespace ORTS.Scripting.Script
{
    public class TVM430_AG : SignalScript
    {
        TVMSpeedType VeAg = TVMSpeedType._320V;

        public override void Initialize()
        {
            if (HasHead(8))
            {
                VeAg = TVMSpeedType._000;
            }
            else if (HasHead(7))
            {
                VeAg = TVMSpeedType._60;
            }
            else if (HasHead(6))
            {
                VeAg = TVMSpeedType._80;
            }
            else if (HasHead(5))
            {
                VeAg = TVMSpeedType._130;
            }
            else if (HasHead(4))
            {
                VeAg = TVMSpeedType._160;
            }
            else if (HasHead(3))
            {
                VeAg = TVMSpeedType._170;
            }
            else if (HasHead(2))
            {
                VeAg = TVMSpeedType._200;
            }
            else if (HasHead(1))
            {
                VeAg = TVMSpeedType._220;
            }
            else
            {
                VeAg = TVMSpeedType._230;
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