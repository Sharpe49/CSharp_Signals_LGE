namespace ORTS.Scripting.Script
{
    public class RM_CFL_TECS_AVL : LuSignalScript
    {
        public override void Update()
        {
            SignalInfo nextNormalSignalInfo = NextNormalSignalInfo;
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");

            if (thisNormalSignalInfo.Aspect == SignalAspect.LU_SFP1)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.LU_SFCCI_O_EFFACE;
            }
            else if (!RouteSet)
            {
                if (nextNormalSignalInfo.Aspect == SignalAspect.LU_SFP1)
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    SignalAspect = SignalAspect.LU_SFAv1;
                    SecondSignalAspect = SignalAspect.LU_SFCCI_O_EFFACE;
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    SignalAspect = SignalAspect.LU_SFAv2;
                    SecondSignalAspect = SignalAspect.LU_SFCCI_O_EFFACE;
                }
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.LU_SFAv2;
                SecondSignalAspect = SignalAspect.LU_SFCCI_O_PRESENTE;
            }

            LuxembourgishTCS();

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}