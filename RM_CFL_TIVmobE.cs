namespace ORTS.Scripting.Script
{
    public class RM_CFL_TIVmobE : LuSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");

            if (thisNormalSignalInfo.Aspect == SignalAspect.LU_SFP1)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.LU_SFI_EFFACE;
            }
            else if (RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.LU_SFI_PRESENTE;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.LU_SFI_EFFACE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}