namespace ORTS.Scripting.Script
{
    public class RM_CFL_ARVL : LuSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo thisRepeaterSignalInfo = DeserializeAspect(SignalId, "REPEATER");
            SignalInfo nextRepeaterSignalInfo = FindSignalAspect("LU_SFVo", "REPEATER", 5);

            if (thisNormalSignalInfo.Aspect == SignalAspect.LU_SFP1)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.None;
            }
            else if (nextNormalSignalInfo.Aspect == SignalAspect.EOA
                || nextNormalSignalInfo.Aspect == SignalAspect.LU_SFP1
                || nextNormalSignalInfo.Aspect == SignalAspect.FR_TABLEAU_G_D)
            {
                MstsSignalAspect = Aspect.Approach_1;
                SignalAspect = SignalAspect.LU_SFAv1;
            }
            else if (nextNormalSignalInfo.Aspect == SignalAspect.LU_SFP3
                || thisRepeaterSignalInfo.Aspect == SignalAspect.LU_SFAvVo_PRESENTE
                || nextRepeaterSignalInfo.Aspect == SignalAspect.LU_SFVo_PRESENTE)
            {
                MstsSignalAspect = Aspect.Approach_2;
                SignalAspect = SignalAspect.LU_SFAv3;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.LU_SFAv2;
            }

            LuxembourgishTCS();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}