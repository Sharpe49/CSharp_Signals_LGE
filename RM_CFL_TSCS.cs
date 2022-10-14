namespace ORTS.Scripting.Script
{
    public class RM_CFL_TSCS : LuSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");

            if (thisNormalSignalInfo.Aspect == SignalAspect.LU_SFP1)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.LU_SFCCI_TF_EFFACE;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.LU_SFCCI_TF_PRESENTE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}