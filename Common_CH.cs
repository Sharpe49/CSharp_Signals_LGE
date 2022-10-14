using System;

namespace ORTS.Scripting.Script
{
    public enum ChInfoAspect
    {
        None,
        CH_SIGNAL_NORMAL,
        CH_SIGNAL_AVANCE,
        CH_SIGNAL_COMBINE,
        CH_SIGNAL_SECTION_DE_LIGNE,
        CH_MARQUEUR_DE_SORTIE_DE_GARE,
        CH_PROCHAIN_SIGNAL_FERME,
        CH_INFO_IMAGE_1,
        CH_INFO_IMAGE_2,
        CH_INFO_IMAGE_3,
        CH_INFO_IMAGE_5,
        CH_INFO_IMAGE_6,
    }

    public abstract class ChSignalScript : SignalScript
    {
        private readonly TextAspectBuilder TextAspectBuilder = new TextAspectBuilder();

        private class SignalState : IEquatable<SignalState>
        {
            public SignalAspect SignalAspect { get; set; } = SignalAspect.None;
            public ChInfoAspect InfoAspect { get; set; } = ChInfoAspect.None;
            public CrocodileState CrocodileState { get; set; } = CrocodileState.None;
            public KvbSigState KvbSigState { get; set; } = KvbSigState.None;
            public KvbTiveState KvbTiveState { get; set; } = KvbTiveState.None;
            public KvbTivdState KvbTivdState { get; set; } = KvbTivdState.None;

            public bool Equals(SignalState other)
            {
                return SignalAspect == other.SignalAspect
                       && InfoAspect == other.InfoAspect
                       && CrocodileState == other.CrocodileState
                       && KvbSigState == other.KvbSigState
                       && KvbTiveState == other.KvbTiveState
                       && KvbTivdState == other.KvbTivdState;
            }

            public void Copy(SignalState other)
            {
                SignalAspect = other.SignalAspect;
                InfoAspect = other.InfoAspect;
                CrocodileState = other.CrocodileState;
                KvbSigState = other.KvbSigState;
                KvbTiveState = other.KvbTiveState;
                KvbTivdState = other.KvbTivdState;
            }
        }

        public SignalAspect SignalAspect
        {
            get => State.SignalAspect;
            set => State.SignalAspect = value;
        }

        public ChInfoAspect InfoAspect
        {
            get => State.InfoAspect;
            set => State.InfoAspect = value;
        }

        public CrocodileState CrocodileState
        {
            get => State.CrocodileState;
            set => State.CrocodileState = value;
        }

        public KvbSigState KvbSigState
        {
            get => State.KvbSigState;
            set => State.KvbSigState = value;
        }

        public KvbTiveState KvbTiveState
        {
            get => State.KvbTiveState;
            set => State.KvbTiveState = value;
        }

        public KvbTivdState KvbTivdState
        {
            get => State.KvbTivdState;
            set => State.KvbTivdState = value;
        }

        private readonly SignalState State = new SignalState();
        private readonly SignalState PreviousState = new SignalState();

        public bool AnnounceByImageW(SignalInfo nextNormalSignalInfo)
        {
            switch (nextNormalSignalInfo.Aspect)
            {
                case SignalAspect.EOA:
                case SignalAspect.CH_IMAGE_H:
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
                case SignalAspect.FR_RR:
                case SignalAspect.FR_RR_A:
                case SignalAspect.FR_RR_ACLI:
                    return true;
            }

            return false;
        }

        public bool AnnounceByImage2(SignalInfo nextNormalSignalInfo)
        {
            switch (nextNormalSignalInfo.Aspect)
            {
                case SignalAspect.CH_IMAGE_2:
                case SignalAspect.CH_IMAGE_6:
                    return true;
            }

            return false;
        }

        public bool AnnounceByImage3(SignalInfo nextNormalSignalInfo)
        {
            switch (nextNormalSignalInfo.Aspect)
            {
                case SignalAspect.CH_IMAGE_3:
                case SignalAspect.FR_RRCLI:
                case SignalAspect.FR_RRCLI_A:
                case SignalAspect.FR_RRCLI_ACLI:
                    return true;
            }

            return false;
        }

        public bool AnnounceByImage5(SignalInfo nextNormalSignalInfo, bool doubleAnnounce = false)
        {
            switch (nextNormalSignalInfo.Aspect)
            {
                case SignalAspect.CH_IMAGE_5:
                    return true;
            }

            return false;
        }

        public void SwissTCS(SignalAspect normalAspect, SignalAspect distantAspect)
        {
            SwissCrocodile(normalAspect, distantAspect);
            SwissKVB(normalAspect, distantAspect);
        }

        public void SwissCombinedTCS(SignalAspect aspect)
        {
            SwissTCS(aspect, aspect);
        }

        public void SwissCrocodile(SignalAspect normalAspect, SignalAspect distantAspect)
        {
            switch (normalAspect)
            {
                case SignalAspect.CH_IMAGE_H:
                case SignalAspect.CH_IMAGE_6:
                    CrocodileState = CrocodileState.CROCODILE_SF;
                    break;

                case SignalAspect.CH_IMAGE_2:
                    switch (distantAspect)
                    {
                        case SignalAspect.CH_IMAGE_W:
                            CrocodileState = CrocodileState.CROCODILE_SF;
                            break;

                        default:
                            CrocodileState = CrocodileState.CROCODILE_SO;
                            break;
                    }
                    break;

                case SignalAspect.CH_IMAGE_3:
                    switch (distantAspect)
                    {
                        case SignalAspect.CH_IMAGE_W:
                        case SignalAspect.CH_IMAGE_2A:
                            CrocodileState = CrocodileState.CROCODILE_SF;
                            break;

                        default:
                            CrocodileState = CrocodileState.CROCODILE_SO;
                            break;
                    }
                    break;

                case SignalAspect.CH_IMAGE_5:
                    switch (distantAspect)
                    {
                        case SignalAspect.CH_IMAGE_W:
                        case SignalAspect.CH_IMAGE_2A:
                        case SignalAspect.CH_IMAGE_3A:
                            CrocodileState = CrocodileState.CROCODILE_SF;
                            break;

                        default:
                            CrocodileState = CrocodileState.CROCODILE_SO;
                            break;
                    }
                    break;

                default:
                    switch (distantAspect)
                    {
                        case SignalAspect.CH_IMAGE_W:
                        case SignalAspect.CH_IMAGE_2A:
                        case SignalAspect.CH_IMAGE_3A:
                        case SignalAspect.CH_IMAGE_5A:
                            CrocodileState = CrocodileState.CROCODILE_SF;
                            break;

                        default:
                            CrocodileState = CrocodileState.CROCODILE_SO;
                            break;
                    }
                    break;
            }
        }

        public void SwissKVB(SignalAspect normalAspect, SignalAspect distantAspect)
        {
            if (normalAspect != SignalAspect.None)
            {
                switch (normalAspect)
                {
                    case SignalAspect.CH_IMAGE_H:
                        KvbSigState = KvbSigState.KVB_S_S_BM;
                        KvbTiveState = KvbTiveState.KVB_TIVE_G_AA;
                        break;

                    case SignalAspect.CH_IMAGE_6:
                        KvbSigState = KvbSigState.KVB_S_A;
                        KvbTiveState = KvbTiveState.KVB_TIVE_G_V40;
                        break;

                    case SignalAspect.CH_IMAGE_2:
                        switch (distantAspect)
                        {
                            case SignalAspect.CH_IMAGE_W:
                                KvbSigState = KvbSigState.KVB_S_A;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V40;
                                break;

                            case SignalAspect.CH_IMAGE_2A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V40;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V40;
                                break;

                            case SignalAspect.CH_IMAGE_3A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V40;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V60;
                                break;

                            case SignalAspect.CH_IMAGE_5A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V40;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V90;
                                break;

                            default:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V40;
                                break;
                        }
                        break;

                    case SignalAspect.CH_IMAGE_3:
                        switch (distantAspect)
                        {
                            case SignalAspect.CH_IMAGE_W:
                                KvbSigState = KvbSigState.KVB_S_A;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V60;
                                break;

                            case SignalAspect.CH_IMAGE_2A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V60;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V40;
                                break;

                            case SignalAspect.CH_IMAGE_3A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V60;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V60;
                                break;

                            case SignalAspect.CH_IMAGE_5A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V60;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V90;
                                break;

                            default:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V60;
                                break;
                        }
                        break;

                    case SignalAspect.CH_IMAGE_5:
                        switch (distantAspect)
                        {
                            case SignalAspect.CH_IMAGE_W:
                                KvbSigState = KvbSigState.KVB_S_A;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V90;
                                break;

                            case SignalAspect.CH_IMAGE_2A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V90;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V40;
                                break;

                            case SignalAspect.CH_IMAGE_3A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V90;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V60;
                                break;

                            case SignalAspect.CH_IMAGE_5A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V90;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V90;
                                break;

                            default:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_V90;
                                break;
                        }
                        break;

                    default:
                        switch (distantAspect)
                        {
                            case SignalAspect.CH_IMAGE_W:
                                KvbSigState = KvbSigState.KVB_S_A;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_AA;
                                break;

                            case SignalAspect.CH_IMAGE_2A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_AA;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V40;
                                break;

                            case SignalAspect.CH_IMAGE_3A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_AA;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V60;
                                break;

                            case SignalAspect.CH_IMAGE_5A:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_AA;
                                KvbTivdState = KvbTivdState.KVB_TIVD_G_V90;
                                break;

                            default:
                                KvbSigState = KvbSigState.KVB_S_VL_INF;
                                KvbTiveState = KvbTiveState.KVB_TIVE_G_AA;
                                break;
                        }
                        break;
                }
            }
            else
            {
                switch (distantAspect)
                {
                    case SignalAspect.CH_IMAGE_W:
                        KvbSigState = KvbSigState.KVB_S_REOCS;
                        break;

                    case SignalAspect.CH_IMAGE_2A:
                        KvbSigState = KvbSigState.KVB_S_REOVL;
                        KvbTivdState = KvbTivdState.KVB_TIVD_G_V40;
                        break;

                    case SignalAspect.CH_IMAGE_3A:
                        KvbSigState = KvbSigState.KVB_S_REOVL;
                        KvbTivdState = KvbTivdState.KVB_TIVD_G_V60;
                        break;

                    case SignalAspect.CH_IMAGE_5A:
                        KvbSigState = KvbSigState.KVB_S_REOVL;
                        KvbTivdState = KvbTivdState.KVB_TIVD_G_V90;
                        break;

                    default:
                        KvbSigState = KvbSigState.KVB_S_REOVL;
                        break;
                }
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

            if (InfoAspect != ChInfoAspect.None)
            {
                TextAspectBuilder.Append(InfoAspect.ToString());
            }

            if (CrocodileState != CrocodileState.None)
            {
                TextAspectBuilder.Append(CrocodileState.ToString());
            }

            if (KvbSigState != KvbSigState.None)
            {
                TextAspectBuilder.Append(KvbSigState.ToString());
            }

            if (KvbTiveState != KvbTiveState.None)
            {
                TextAspectBuilder.Append(KvbTiveState.ToString());
            }

            if (KvbTivdState != KvbTivdState.None)
            {
                TextAspectBuilder.Append(KvbTivdState.ToString());
            }

            TextSignalAspect = TextAspectBuilder.ToString();

            PreviousState.Copy(State);
        }
    }
}
