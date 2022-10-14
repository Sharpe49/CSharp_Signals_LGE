namespace ORTS.Scripting.Script
{
    public class RM_CFL_TIVmobA : LuSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");
            SignalInfo nextTivrSignalInfo = DeserializeAspect(NextSignalId("TIVR"), "TIVR");

            if (thisNormalSignalInfo.Aspect == SignalAspect.LU_SFP1)
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.LU_SFAvI_EFFACE;
            }
            else if (nextTivrSignalInfo.Aspect == SignalAspect.LU_SFI_PRESENTE)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.LU_SFAvI_PRESENTE;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.LU_SFAvI_EFFACE;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}