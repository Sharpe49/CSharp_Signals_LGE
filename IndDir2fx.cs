namespace ORTS.Scripting.Script
{
    public class IndDir2fx : FrSignalScript
    {
        public override void Update()
        {
            SignalInfo thisNormalSignalInfo = DeserializeAspect(SignalId, "NORMAL");

            if (!Enabled || thisNormalSignalInfo.Aspect == SignalAspect.FR_C_BAL)
            {
                MstsSignalAspect = Aspect.Stop;
                SignalAspect = SignalAspect.FR_ID_ETEINT;
            }
            else if (!RouteSet)
            {
                MstsSignalAspect = Aspect.Clear_1;
                SignalAspect = SignalAspect.FR_ID_1_FEU;
            }
            else
            {
                MstsSignalAspect = Aspect.Clear_2;
                SignalAspect = SignalAspect.FR_ID_2_FEUX;
            }

            SerializeAspect();
            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}