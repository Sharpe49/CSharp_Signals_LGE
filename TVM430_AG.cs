using Orts.Simulation.Signalling;
using System;
using System.Collections.Generic;
using System.Linq;
using static ORTS.Scripting.Script.TVM430Common;

namespace ORTS.Scripting.Script
{
    public class TVM430_AG : CsSignalScript
    {
        TVMSpeedType VeAg = TVMSpeedType._320V;

        public TVM430_AG()
        {
        }

        public override void Initialize()
        {
        }

        public override void Update()
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

            MstsSignalAspect = Aspect.Clear_2;
            TextSignalAspect = "FR_TVM430_AG Ve" + VeAg.ToString().Substring(1);
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}