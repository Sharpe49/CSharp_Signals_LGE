using System;
using Orts.Simulation.Signalling;

namespace ORTS.Scripting.Script
{
    public enum CrocodileState
    {
        None,
        CROCODILE_SF,
        CROCODILE_SO
    }

    public enum KvbVanState
    {
        None,
        KVB_VAN_V30,
        KVB_VAN_V40,
        KVB_VAN_V50,
        KVB_VAN_V60,
        KVB_VAN_V70,
        KVB_VAN_V80,
        KVB_VAN_V90,
        KVB_VAN_V100,
        KVB_VAN_V110,
        KVB_VAN_V120,
        KVB_VAN_V130,
        KVB_VAN_V140,
        KVB_VAN_V150,
        KVB_VAN_V160,
        KVB_VPMOB,
    }

    public enum KvbVraState
    {
        None,
        KVB_VRA_V30,
        KVB_VRA_V40,
        KVB_VRA_V50,
        KVB_VRA_V60,
        KVB_VRA_V70,
        KVB_VRA_V80,
        KVB_VRA_V90,
        KVB_VRA_V100,
        KVB_VRA_V110,
        KVB_VRA_V120,
        KVB_VRA_V130,
        KVB_VRA_V140,
        KVB_VRA_V150,
        KVB_VRA_V160,
        KVB_VRA_AA,
        KVB_TPAA,
    }
    
    public enum KvbSigState
    {
        None,
        KVB_S_C_BAL,
        KVB_S_S_BM,
        KVB_S_S_BAL,
        KVB_S_A,
        KVB_S_ACLI,
        KVB_S_VLCLI,
        KVB_S_VL_INF,
        KVB_S_VL_SUP,
        KVB_S_REOCS,
        KVB_S_REOVL
    }

    public enum KvbTivdState
    {
        None,
        KVB_TIVD_G_V40,
        KVB_TIVD_G_V60,
        KVB_TIVD_G_V90,
        KVB_PFIX,
    }

    public enum KvbTiveState
    {
        None,
        KVB_TIVE_G_V40,
        KVB_TIVE_G_V60,
        KVB_TIVE_G_V90,
        KVB_TIVE_G_AA,
        KVB_TPEX,
    }

    public enum KvbDivState
    {
        None,
        KVB_DGV,
        KVB_FGV,
        KVB_DVL,
        KVB_FVL,
        KVB_FZ,
    }

    public enum KvbCsspMessage
    {
        None,
        KVB_CSSP_LP,
        KVB_CSSP_BP,
        KVB_CSSP_AODJA,
        KVB_CSSP_FODJA,
    }

    public enum SilecMessage
    {
        None,
        SILEC_LP,
        SILEC_BP,
        SILEC_AODJA,
        SILEC_FODJA,
    }

    public enum Tvm300EpiMessage
    {
        None,
        EPI_ECS,
        EPI_ECR,
        EPI_EB,
        EPI_NF,
        EPI_KV65,
        EPI_BP,
        EPI_CCT,
    }

    public enum Tvm430BspMessage
    {
        None,
        BSP_ECS,
        BSP_ESL,
        BSP_ESL2,
        BSP_C300,
        BSP_C430,
        BSP_CNf,
        BSP_KV22,
        BSP_ABP,
        BSP_ELC25,
        BSP_ELC1_5,
        BSP_ELGV,
        BSP_ELC3,
        BSP_EET,
        BSP_AODJ,
        BSP_EODJ,
    }

    public abstract class FrSignalScript : SignalScript
    {
        private readonly TextAspectBuilder TextAspectBuilder = new TextAspectBuilder();

        private class SignalState : IEquatable<SignalState>
        {
            public SignalAspect SignalAspect { get; set; } = SignalAspect.None;
            public TvmType TvmType { get; set; } = TvmType.None;
            public TvmSpeedType VeE { get; set; } = TvmSpeedType.None;
            public TvmSpeedType VcE { get; set; } = TvmSpeedType.None;
            public TvmSpeedType VaE { get; set; } = TvmSpeedType.None;
            public DirectionInfoAspect DirectionInfoAspect { get; set; } = DirectionInfoAspect.None;
            public SpeedInfoAspect SpeedInfoAspect { get; set; } = SpeedInfoAspect.None;
            public IpcsInfoAspect IpcsInfoAspect { get; set; } = IpcsInfoAspect.None;
            public CrocodileState CrocodileState { get; set; } = CrocodileState.None;
            public KvbVanState KvbVanState { get; set; } = KvbVanState.None;
            public KvbVraState KvbVraState { get; set; } = KvbVraState.None;
            public KvbSigState KvbSigState { get; set; } = KvbSigState.None;
            public KvbDivState KvbDivState { get; set; } = KvbDivState.None;
            public KvbCsspMessage KvbCsspMessage { get; set; } = KvbCsspMessage.None;
            public SilecMessage SilecMessage { get; set; } = SilecMessage.None;
            public Tvm300EpiMessage Tvm300EpiMessage { get; set; } = Tvm300EpiMessage.None;
            public Tvm430BspMessage Tvm430BspMessage { get; set; } = Tvm430BspMessage.None;
            public bool ESUBO { get; set; } = false;

            public bool Equals(SignalState other)
            {
                return SignalAspect == other.SignalAspect
                       && TvmType == other.TvmType
                       && VeE == other.VeE
                       && VcE == other.VcE
                       && VaE == other.VaE
                       && DirectionInfoAspect == other.DirectionInfoAspect
                       && SpeedInfoAspect == other.SpeedInfoAspect
                       && IpcsInfoAspect == other.IpcsInfoAspect
                       && CrocodileState == other.CrocodileState
                       && KvbVanState == other.KvbVanState
                       && KvbVraState == other.KvbVraState
                       && KvbSigState == other.KvbSigState
                       && KvbDivState == other.KvbDivState
                       && KvbCsspMessage == other.KvbCsspMessage
                       && SilecMessage == other.SilecMessage
                       && Tvm300EpiMessage == other.Tvm300EpiMessage
                       && Tvm430BspMessage == other.Tvm430BspMessage
                       && ESUBO == other.ESUBO;
            }

            public void Copy(SignalState other)
            {
                SignalAspect = other.SignalAspect;
                TvmType = other.TvmType;
                VeE = other.VeE;
                VcE = other.VcE;
                VaE = other.VaE;
                DirectionInfoAspect = other.DirectionInfoAspect;
                SpeedInfoAspect = other.SpeedInfoAspect;
                IpcsInfoAspect = other.IpcsInfoAspect;
                CrocodileState = other.CrocodileState;
                KvbVanState = other.KvbVanState;
                KvbVraState = other.KvbVraState;
                KvbSigState = other.KvbSigState;
                KvbDivState = other.KvbDivState;
                KvbCsspMessage = other.KvbCsspMessage;
                SilecMessage = other.SilecMessage;
                Tvm300EpiMessage = other.Tvm300EpiMessage;
                Tvm430BspMessage = other.Tvm430BspMessage;
                ESUBO = other.ESUBO;
            }
        }

        public SignalAspect SignalAspect
        {
            get => State.SignalAspect;
            set => State.SignalAspect = value;
        }

        public TvmType TvmType
        {
            get => State.TvmType;
            set => State.TvmType = value;
        }

        public TvmSpeedType VeE
        {
            get => State.VeE;
            set => State.VeE = value;
        }

        public TvmSpeedType VcE
        {
            get => State.VcE;
            set => State.VcE = value;
        }

        public TvmSpeedType VaE
        {
            get => State.VaE;
            set => State.VaE = value;
        }

        public DirectionInfoAspect DirectionInfoAspect
        {
            get => State.DirectionInfoAspect;
            set => State.DirectionInfoAspect = value;
        }

        public SpeedInfoAspect SpeedInfoAspect
        {
            get => State.SpeedInfoAspect;
            set => State.SpeedInfoAspect = value;
        }

        public IpcsInfoAspect IpcsInfoAspect
        {
            get => State.IpcsInfoAspect;
            set => State.IpcsInfoAspect = value;
        }

        public CrocodileState CrocodileState
        {
            get => State.CrocodileState;
            set => State.CrocodileState = value;
        }

        public KvbVanState KvbVanState
        {
            get => State.KvbVanState;
            set => State.KvbVanState = value;
        }

        public KvbVraState KvbVraState
        {
            get => State.KvbVraState;
            set => State.KvbVraState = value;
        }

        public KvbSigState KvbSigState
        {
            get => State.KvbSigState;
            set => State.KvbSigState = value;
        }

        public KvbDivState KvbDivState
        {
            get => State.KvbDivState;
            set => State.KvbDivState = value;
        }

        public KvbCsspMessage KvbCsspMessage
        {
            get => State.KvbCsspMessage;
            set => State.KvbCsspMessage = value;
        }

        public SilecMessage SilecMessage
        {
            get => State.SilecMessage;
            set => State.SilecMessage = value;
        }

        public Tvm300EpiMessage Tvm300EpiMessage
        {
            get => State.Tvm300EpiMessage;
            set => State.Tvm300EpiMessage = value;
        }

        public Tvm430BspMessage Tvm430BspMessage
        {
            get => State.Tvm430BspMessage;
            set => State.Tvm430BspMessage = value;
        }

        public bool ESUBO
        {
            get => State.ESUBO;
            set => State.ESUBO = value;
        }

        private readonly SignalState State = new SignalState();
        private readonly SignalState PreviousState = new SignalState();

        public bool CommandAspectC(SignalInfo nextNormalSignalInfo, bool absoluteBlock = false, bool automatic = false) =>
                !automatic && !Enabled
                || CurrentBlockState == BlockState.Obstructed
                || absoluteBlock && CurrentBlockState == BlockState.Occupied
                || nextNormalSignalInfo.Aspect == SignalAspect.FR_FSO
                || HoldState == HoldState.StationStop
                || HoldState == HoldState.ManualLock;

        public bool CommandAspectS() => CurrentBlockState != BlockState.Clear
                || HoldState == HoldState.ManualLock;

        public bool AnnounceByA(SignalInfo nextNormalSignalInfo, bool announceRR = true, bool announceRRCLI = true)
        {
            if (HoldState == HoldState.ManualApproach)
            {
                return true;
            }

            switch (nextNormalSignalInfo.Aspect)
            {
                case SignalAspect.EOA:
                case SignalAspect.FR_C_BAL:
                case SignalAspect.FR_C_BAPR:
                case SignalAspect.FR_C_BM:
                case SignalAspect.FR_CV:
                case SignalAspect.FR_S_BAL:
                case SignalAspect.FR_S_BAPR:
                case SignalAspect.FR_S_BM:
                case SignalAspect.FR_SCLI:
                case SignalAspect.FR_MCLI:
                case SignalAspect.FR_M:
                    return true;

                case SignalAspect.FR_RR_A:
                case SignalAspect.FR_RR_ACLI:
                case SignalAspect.FR_RR:
                    return announceRR;

                case SignalAspect.FR_RRCLI_A:
                case SignalAspect.FR_RRCLI_ACLI:
                case SignalAspect.FR_RRCLI:
                    return announceRRCLI;
            }

            return false;
        }

        public bool AnnounceByACLI(SignalInfo nextNormalSignalInfo)
        {
            switch (nextNormalSignalInfo.Aspect)
            {
                case SignalAspect.FR_A:
                case SignalAspect.FR_R:
                    return true;
            }

            return false;
        }

        public bool AnnounceByVLCLI(SignalInfo nextNormalSignalInfo)
        {
            switch (nextNormalSignalInfo.Aspect)
            {
                case SignalAspect.FR_A:
                case SignalAspect.FR_R:
                case SignalAspect.FR_ACLI:
                case SignalAspect.FR_RCLI:
                case SignalAspect.FR_RCLI_ACLI:
                    return true;
            }

            return false;
        }

        public bool AnnounceByR(SignalInfo nextNormalSignalInfo, bool doubleAnnounce = false)
        {
            switch (nextNormalSignalInfo.Aspect)
            {
                case SignalAspect.FR_RR:
                case SignalAspect.FR_RR_A:
                case SignalAspect.FR_RR_ACLI:
                    return true;

                case SignalAspect.FR_R:
                    return doubleAnnounce;
            }

            return false;
        }

        public bool AnnounceByRCLI(SignalInfo nextNormalSignalInfo, bool doubleAnnounce = false)
        {
            switch (nextNormalSignalInfo.Aspect)
            {
                case SignalAspect.FR_RRCLI:
                case SignalAspect.FR_RRCLI_A:
                case SignalAspect.FR_RRCLI_ACLI:
                    return true;

                case SignalAspect.FR_RCLI:
                    return doubleAnnounce;
            }

            return false;
        }

        public bool AnnounceByRCLI_ACLI(SignalInfo nextNormalSignalInfo)
        {
            switch (nextNormalSignalInfo.Aspect)
            {
                case SignalAspect.FR_RRCLI_A:
                    return true;
            }

            return false;
        }

        protected void FrenchTcs(bool signalHasSpeedRepeater = false, bool distantSignal = false)
        {
            FrenchCrocodile();
            FrenchKvb(signalHasSpeedRepeater, distantSignal);
        }

        protected void FrenchCrocodile()
        {
            switch (State.SignalAspect)
            {
                case SignalAspect.FR_C_BAL:
                case SignalAspect.FR_C_BAPR:
                case SignalAspect.FR_C_BM:
                case SignalAspect.FR_CV:
                case SignalAspect.FR_S_BAL:
                case SignalAspect.FR_S_BAPR:
                case SignalAspect.FR_S_BM:
                case SignalAspect.FR_SCLI:
                case SignalAspect.FR_MCLI:
                case SignalAspect.FR_M:
                case SignalAspect.FR_D:
                case SignalAspect.FR_RR_A:
                case SignalAspect.FR_RR_ACLI:
                case SignalAspect.FR_RRCLI_A:
                case SignalAspect.FR_RRCLI_ACLI:
                case SignalAspect.FR_A:
                case SignalAspect.FR_R:
                case SignalAspect.FR_ACLI:
                case SignalAspect.FR_RCLI:
                case SignalAspect.FR_RCLI_ACLI:
                case SignalAspect.FR_TIVD_PRESENTE:
                case SignalAspect.LU_SFP1:
                case SignalAspect.LU_SFAv1:
                case SignalAspect.LU_SFAv3:
                    State.CrocodileState = CrocodileState.CROCODILE_SF;
                    break;

                default:
                    State.CrocodileState = CrocodileState.CROCODILE_SO;
                    break;
            }
        }

        protected void FrenchKvb(bool signalHasSpeedRepeater = false, bool distantSignal = false, bool diskSignal = false)
        {
            if (diskSignal)
            {
                return;
            }

            // VAN
            switch (SignalAspect)
            {
                case SignalAspect.FR_R:
                    KvbVanState = KvbVanState.KVB_VAN_V30;
                    break;

                case SignalAspect.FR_RCLI:
                case SignalAspect.FR_RCLI_ACLI:
                    KvbVanState = KvbVanState.KVB_VAN_V60;
                    break;

                default:
                    KvbVanState = KvbVanState.None;
                    break;
            }

            if (distantSignal)
            {
                // S
                switch (SignalAspect)
                {
                    case SignalAspect.FR_D:
                    case SignalAspect.FR_A:
                    case SignalAspect.FR_ACLI:
                    case SignalAspect.FR_RCLI_ACLI:
                        KvbSigState = KvbSigState.KVB_S_REOCS;
                        break;

                    default:
                        KvbSigState = KvbSigState.KVB_S_REOVL;
                        break;
                }
            }
            else
            {
                // VRA
                switch (SignalAspect)
                {
                    case SignalAspect.FR_RR_A:
                    case SignalAspect.FR_RR_ACLI:
                    case SignalAspect.FR_RR:
                        KvbVraState = KvbVraState.KVB_VRA_V30;
                        break;

                    case SignalAspect.FR_RRCLI_A:
                    case SignalAspect.FR_RRCLI_ACLI:
                    case SignalAspect.FR_RRCLI:
                        KvbVraState = KvbVraState.KVB_VRA_V60;
                        break;

                    default:
                        KvbVraState = signalHasSpeedRepeater ? KvbVraState.KVB_VRA_AA : KvbVraState.None;
                        break;
                }

                switch (SignalAspect)
                {
                    case SignalAspect.FR_C_BAL:
                    case SignalAspect.FR_C_BAPR:
                    case SignalAspect.FR_CV:
                        KvbSigState = KvbSigState.KVB_S_C_BAL;
                        break;

                    case SignalAspect.FR_C_BM:
                    case SignalAspect.FR_S_BM:
                        KvbSigState = KvbSigState.KVB_S_S_BM;
                        break;

                    case SignalAspect.FR_S_BAL:
                    case SignalAspect.FR_S_BAPR:
                    case SignalAspect.FR_SCLI:
                    case SignalAspect.FR_MCLI:
                    case SignalAspect.FR_M:
                        KvbSigState = KvbSigState.KVB_S_S_BAL;
                        break;

                    case SignalAspect.FR_RR_A:
                    case SignalAspect.FR_RRCLI_A:
                    case SignalAspect.FR_A:
                        KvbSigState = KvbSigState.KVB_S_A;
                        break;

                    case SignalAspect.FR_RR_ACLI:
                    case SignalAspect.FR_RRCLI_ACLI:
                    case SignalAspect.FR_RCLI_ACLI:
                    case SignalAspect.FR_ACLI:
                        KvbSigState = KvbSigState.KVB_S_ACLI;
                        break;

                    case SignalAspect.FR_VLCLI_ANN:
                    case SignalAspect.FR_VLCLI_EXE:
                        KvbSigState = KvbSigState.KVB_S_VLCLI;
                        break;

                    case SignalAspect.FR_RR:
                    case SignalAspect.FR_RRCLI:
                    case SignalAspect.FR_R:
                    case SignalAspect.FR_RCLI:
                    case SignalAspect.FR_VL_INF:
                        KvbSigState = KvbSigState.KVB_S_VL_INF;
                        break;

                    case SignalAspect.FR_VL_SUP:
                        KvbSigState = KvbSigState.KVB_S_VL_SUP;
                        break;

                    default:
                        KvbSigState = KvbSigState.None;
                        break;
                }
            }
        }

        protected void FrenchKvbTivd(int speedKpH)
        {
            if (SignalAspect == SignalAspect.FR_TIVD_PRESENTE)
            {
                KvbVanState = (KvbVanState)Enum.Parse(typeof(KvbVanState), $"KVB_VAN_V{speedKpH}");
            }
            else
            {
                KvbVanState = KvbVanState.None;
            }
        }

        protected void FrenchKvbTivr(int speedKpH)
        {
            if (SignalAspect == SignalAspect.FR_TIVR_PRESENTE)
            {
                KvbVraState = (KvbVraState) Enum.Parse(typeof(KvbVraState), $"KVB_VRA_V{speedKpH}");
            }
            else
            {
                KvbVraState = KvbVraState.KVB_VRA_AA;
            }
        }

        protected void FrenchKvbCssp()
        {
            switch (SignalAspect)
            {
                case SignalAspect.FR_BP_ANNONCE_PRESENTE:
                    KvbCsspMessage = KvbCsspMessage.KVB_CSSP_BP;
                    break;

                case SignalAspect.FR_BP_FP_1500V_PRESENTE:
                case SignalAspect.FR_BP_FP_3000V_PRESENTE:
                case SignalAspect.FR_BP_FP_25000V_PRESENTE:
                case SignalAspect.FR_BP_FP_25000VLGV_PRESENTE:
                case SignalAspect.FR_BP_FP_25000VET_PRESENTE:
                    KvbCsspMessage = KvbCsspMessage.KVB_CSSP_LP;
                    break;

                case SignalAspect.FR_CCT_ANNONCE_PRESENTE:
                    KvbCsspMessage = KvbCsspMessage.KVB_CSSP_AODJA;
                    break;

                case SignalAspect.FR_CCT_FP_PRESENTE:
                    KvbCsspMessage = KvbCsspMessage.KVB_CSSP_FODJA;
                    break;

                default:
                    KvbCsspMessage = KvbCsspMessage.None;
                    break;
            }
        }

        protected void FrenchSilec()
        {
            switch (SignalAspect)
            {
                case SignalAspect.FR_BP_ANNONCE_PRESENTE:
                    SilecMessage = SilecMessage.SILEC_BP;
                    break;

                case SignalAspect.FR_BP_FP_1500V_PRESENTE:
                case SignalAspect.FR_BP_FP_3000V_PRESENTE:
                case SignalAspect.FR_BP_FP_25000V_PRESENTE:
                case SignalAspect.FR_BP_FP_25000VLGV_PRESENTE:
                case SignalAspect.FR_BP_FP_25000VET_PRESENTE:
                    SilecMessage = SilecMessage.SILEC_LP;
                    break;

                case SignalAspect.FR_CCT_ANNONCE_PRESENTE:
                    SilecMessage = SilecMessage.SILEC_AODJA;
                    break;

                case SignalAspect.FR_CCT_FP_PRESENTE:
                    SilecMessage = SilecMessage.SILEC_FODJA;
                    break;

                default:
                    SilecMessage = SilecMessage.None;
                    break;
            }
        }

        protected void FrenchTvm300Epi()
        {
            switch (SignalAspect)
            {
                case SignalAspect.FR_BP_ANNONCE_PRESENTE:
                    Tvm300EpiMessage = Tvm300EpiMessage.EPI_BP;
                    break;

                case SignalAspect.FR_CCT_ANNONCE_PRESENTE:
                    Tvm300EpiMessage = Tvm300EpiMessage.EPI_CCT;
                    break;

                default:
                    Tvm300EpiMessage = Tvm300EpiMessage.None;
                    break;
            }
        }

        protected void FrenchTvm430Bsp()
        {
            switch (SignalAspect)
            {
                case SignalAspect.FR_BP_ANNONCE_PRESENTE:
                    Tvm430BspMessage = Tvm430BspMessage.BSP_ABP;
                    break;

                case SignalAspect.FR_CCT_ANNONCE_PRESENTE:
                    Tvm430BspMessage = Tvm430BspMessage.BSP_AODJ;
                    break;

                case SignalAspect.FR_CCT_EXECUTION_PRESENTE:
                    Tvm430BspMessage = Tvm430BspMessage.BSP_EODJ;
                    break;

                default:
                    Tvm430BspMessage = Tvm430BspMessage.None;
                    break;
            }
        }

        protected void SerializeAspect()
        {
            if (State.Equals(PreviousState))
            {
                return;
            }

            TextAspectBuilder.Clear();

            if (SignalAspect != SignalAspect.None)
            {
                TextAspectBuilder.Append(SignalAspect.ToString());
            }

            if (TvmType != TvmType.None)
            {
                TextAspectBuilder.Append(TvmType.ToString());
            }

            if (VeE != TvmSpeedType.None)
            {
                TextAspectBuilder.Append("Ve" + VeE.ToString().Substring(1));
            }

            if (VcE != TvmSpeedType.None)
            {
                TextAspectBuilder.Append("Vc" + VcE.ToString().Substring(1));
            }

            if (VaE != TvmSpeedType.None && VaE != TvmSpeedType.Any)
            {
                TextAspectBuilder.Append("Va" + VaE.ToString().Substring(1));
            }

            if (DirectionInfoAspect != DirectionInfoAspect.None)
            {
                TextAspectBuilder.Append(DirectionInfoAspect.ToString());
            }

            if (SpeedInfoAspect != SpeedInfoAspect.None)
            {
                TextAspectBuilder.Append(SpeedInfoAspect.ToString());
            }

            if (IpcsInfoAspect != IpcsInfoAspect.None)
            {
                TextAspectBuilder.Append(IpcsInfoAspect.ToString());
            }

            if (CrocodileState != CrocodileState.None)
            {
                TextAspectBuilder.Append(CrocodileState.ToString());
            }

            if (KvbVanState != KvbVanState.None)
            {
                TextAspectBuilder.Append(KvbVanState.ToString());
            }

            if (KvbVraState != KvbVraState.None)
            {
                TextAspectBuilder.Append(KvbVraState.ToString());
            }

            if (KvbSigState != KvbSigState.None)
            {
                TextAspectBuilder.Append(KvbSigState.ToString());
            }

            if (KvbDivState != KvbDivState.None)
            {
                TextAspectBuilder.Append(KvbDivState.ToString());
            }

            if (KvbCsspMessage != KvbCsspMessage.None)
            {
                TextAspectBuilder.Append(KvbCsspMessage.ToString());
            }

            if (SilecMessage != SilecMessage.None)
            {
                TextAspectBuilder.Append(SilecMessage.ToString());
            }

            if (Tvm300EpiMessage != Tvm300EpiMessage.None)
            {
                TextAspectBuilder.Append(Tvm300EpiMessage.ToString());
            }

            if (Tvm430BspMessage != Tvm430BspMessage.None)
            {
                TextAspectBuilder.Append(Tvm430BspMessage.ToString());
            }

            if (ESUBO)
            {
                TextAspectBuilder.Append("ESUBO");
            }

            TextSignalAspect = TextAspectBuilder.ToString();

            PreviousState.Copy(State);
        }
    }
}
