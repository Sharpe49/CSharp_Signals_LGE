namespace ORTS.Scripting.Script
{
    public class RM_CFL_SRARRMVL_TIV : LuSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisSpeedSignalInfo = DeserializeAspect(SignalId, "TIVR");
            SignalInfo directionSpeedInfo = FindSignalAspect("DIR", "INFO", 5);

            if (!Enabled
                || CurrentBlockState != BlockState.Clear)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.LU_SFP1;
                SecondSignalAspect = SignalAspect.LU_SFVb1;
            }
            else if (directionSpeedInfo.DirectionInfoAspect == DirectionInfoAspect.DIR5)
            {
                MstsSignalAspect = Aspect.Restricting;
                SignalAspect = SignalAspect.LU_SFP1;
                SecondSignalAspect = SignalAspect.LU_SFVb2;
            }
            else if (nextNormalSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D)
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.LU_SFP3;
                SecondSignalAspect = SignalAspect.LU_SFVb2;
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.LU_SFP3;
                SecondSignalAspect = SignalAspect.LU_SFVb2;
            }
            else if (thisSpeedSignalInfo.Aspect == SignalAspect.LU_SFI_PRESENTE)
            {
                MstsSignalAspect = Aspect.Approach_3;
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