namespace ORTS.Scripting.Script
{
    public class RM_CFL_SRARRMVL : LuSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisRepeaterSignalInfo = DeserializeAspect(SignalId, "REPEATER");
            SignalInfo directionSignalInfo = FindSignalAspect("DIR", "INFO", 5);

            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.LU_SFP1;
                SecondSignalAspect = SignalAspect.LU_SFVb1;
            }
            else if (directionSignalInfo.DirectionInfoAspect == DirectionInfoAspect.DIR5)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = SignalAspect.LU_SFP1;
                SecondSignalAspect = SignalAspect.LU_SFVb2;
            }
            else if (!RouteSet
                || nextNormalSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D
                || thisRepeaterSignalInfo.Aspect == SignalAspect.LU_SFVo_PRESENTE
                || IsSignalFeatureEnabled("USER2"))
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.LU_SFP3;
                SecondSignalAspect = SignalAspect.LU_SFVb2;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.LU_SFP2;
                SecondSignalAspect = SignalAspect.LU_SFVb2;
            }

            LuxembourgishTCS();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}