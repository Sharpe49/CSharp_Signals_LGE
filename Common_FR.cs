using System;
using Orts.Simulation.Signalling;
using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public enum FrSignalAspect
    {
        None,
        FR_C_BAL,
        FR_C_BAPR,
        FR_C_BM,
        FR_CV,
        FR_S_BAL,
        FR_S_BAPR,
        FR_S_BM,
        FR_SCLI,
        FR_MCLI,
        FR_M,
        FR_D,
        FR_RR_A,
        FR_RR_ACLI,
        FR_RR,
        FR_RRCLI_A,
        FR_RRCLI_ACLI,
        FR_RRCLI,
        FR_A,
        FR_R,
        FR_ACLI,
        FR_RCLI,
        FR_RCLI_ACLI,
        FR_VLCLI_ANN,
        FR_VLCLI_EXE,
        FR_VL_INF,
        FR_VL_SUP,
        LU_SFP1,
        LU_SFAv1,
        LU_SFAv3,
    }

    public enum FrCrocodileState
    {
        None,
        CROCODILE_SF,
        CROCODILE_SO
    }

    public enum FrKvbVanState
    {
        None,
        KVB_VAN_V30,
        KVB_VAN_V60
    }

    public enum FrKvbVraState
    {
        None,
        KVB_VRA_V30,
        KVB_VRA_V60,
        KVB_VRA_AA
    }

    public enum FrKvbSigState
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

    public abstract class FrSignalScript : SignalScript
    {
        private readonly TextAspectBuilder TextAspectBuilder = new TextAspectBuilder();

        public FrSignalAspect SignalAspect { get; protected set; } = FrSignalAspect.None;
        public FrCrocodileState CrocodileState { get; protected set; } = FrCrocodileState.None;
        public FrKvbVanState KvbVanState { get; protected set; } = FrKvbVanState.None;
        public FrKvbVraState KvbVraState { get; protected set; } = FrKvbVraState.None;
        public FrKvbSigState KvbSigState { get; protected set; } = FrKvbSigState.None;

        public bool CommandAspectC(List<string> nextNormalParts, bool absoluteBlock = false, bool automatic = false) =>
                !automatic && !Enabled
                || CurrentBlockState == BlockState.Obstructed
                || absoluteBlock && CurrentBlockState == BlockState.Occupied
                || nextNormalParts.Contains("FR_FSO")
                || HoldState == HoldState.StationStop
                || HoldState == HoldState.ManualLock;

        public bool CommandAspectS() => CurrentBlockState != BlockState.Clear
                || HoldState == HoldState.ManualLock;

        public bool AnnounceByA(List<string> aspects, bool announceRR = true, bool announceRRCLI = true)
        {
            if (HoldState == HoldState.ManualApproach)
            {
                return true;
            }

            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "EOA":
                    case "FR_C_BAL":
                    case "FR_C_BAPR":
                    case "FR_C_BM":
                    case "FR_CV":
                    case "FR_S_BAL":
                    case "FR_S_BAPR":
                    case "FR_S_BM":
                    case "FR_SCLI":
                    case "FR_MCLI":
                    case "FR_M":
                        return true;

                    case "FR_RR_A":
                    case "FR_RR_ACLI":
                    case "FR_RR":
                        return announceRR;

                    case "FR_RRCLI_A":
                    case "FR_RRCLI_ACLI":
                    case "FR_RRCLI":
                        return announceRRCLI;
                }
            }

            return false;
        }

        public bool AnnounceByACLI(List<string> aspects)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "FR_A":
                    case "FR_R":
                        return true;
                }
            }

            return false;
        }

        public bool AnnounceByVLCLI(List<string> aspects)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "FR_A":
                    case "FR_R":
                    case "FR_ACLI":
                    case "FR_RCLI":
                    case "FR_RCLI_ACLI":
                        return true;
                }
            }

            return false;
        }

        public bool AnnounceByR(List<string> aspects, bool doubleAnnounce = false)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "FR_RR":
                    case "FR_RR_A":
                    case "FR_RR_ACLI":
                        return true;

                    case "FR_R":
                        return doubleAnnounce;
                }
            }

            return false;
        }

        public bool AnnounceByRCLI(List<string> aspects, bool doubleAnnounce = false)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "FR_RRCLI":
                    case "FR_RRCLI_A":
                    case "FR_RRCLI_ACLI":
                        return true;

                    case "FR_RCLI":
                        return doubleAnnounce;
                }
            }

            return false;
        }

        public bool AnnounceByRCLI_ACLI(List<string> aspects)
        {
            foreach (string aspect in aspects)
            {
                switch (aspect)
                {
                    case "FR_RRCLI_A":
                        return true;
                }
            }

            return false;
        }

        protected void FrenchTCS(bool signalHasSpeedRepeater = false, bool distantSignal = false)
        {
            FrenchCrocodile();
            FrenchKVB(signalHasSpeedRepeater, distantSignal);
        }

        protected void FrenchCrocodile()
        {
            switch (SignalAspect)
            {
                case FrSignalAspect.FR_C_BAL:
                case FrSignalAspect.FR_C_BAPR:
                case FrSignalAspect.FR_C_BM:
                case FrSignalAspect.FR_CV:
                case FrSignalAspect.FR_S_BAL:
                case FrSignalAspect.FR_S_BAPR:
                case FrSignalAspect.FR_S_BM:
                case FrSignalAspect.FR_SCLI:
                case FrSignalAspect.FR_MCLI:
                case FrSignalAspect.FR_M:
                case FrSignalAspect.FR_D:
                case FrSignalAspect.FR_RR_A:
                case FrSignalAspect.FR_RR_ACLI:
                case FrSignalAspect.FR_RRCLI_A:
                case FrSignalAspect.FR_RRCLI_ACLI:
                case FrSignalAspect.FR_A:
                case FrSignalAspect.FR_R:
                case FrSignalAspect.FR_ACLI:
                case FrSignalAspect.FR_RCLI:
                case FrSignalAspect.FR_RCLI_ACLI:
                case FrSignalAspect.LU_SFP1:
                case FrSignalAspect.LU_SFAv1:
                case FrSignalAspect.LU_SFAv3:
                    CrocodileState = FrCrocodileState.CROCODILE_SF;
                    break;

                default:
                    CrocodileState = FrCrocodileState.CROCODILE_SO;
                    break;
            }
        }

        protected void FrenchKVB(bool signalHasSpeedRepeater = false, bool distantSignal = false, bool diskSignal = false)
        {
            if (diskSignal)
            {
                return;
            }

            // VAN
            switch (SignalAspect)
            {
                case FrSignalAspect.FR_R:
                    KvbVanState = FrKvbVanState.KVB_VAN_V30;
                    break;

                case FrSignalAspect.FR_RCLI:
                case FrSignalAspect.FR_RCLI_ACLI:
                    KvbVanState = FrKvbVanState.KVB_VAN_V60;
                    break;

                default:
                    KvbVanState = FrKvbVanState.None;
                    break;
            }

            if (distantSignal)
            {
                // S
                switch (SignalAspect)
                {
                    case FrSignalAspect.FR_D:
                    case FrSignalAspect.FR_A:
                    case FrSignalAspect.FR_ACLI:
                    case FrSignalAspect.FR_RCLI_ACLI:
                        KvbSigState = FrKvbSigState.KVB_S_REOCS;
                        break;

                    default:
                        KvbSigState = FrKvbSigState.KVB_S_REOVL;
                        break;
                }
            }
            else
            {
                // VRA
                switch (SignalAspect)
                {
                    case FrSignalAspect.FR_RR_A:
                    case FrSignalAspect.FR_RR_ACLI:
                    case FrSignalAspect.FR_RR:
                        KvbVraState = FrKvbVraState.KVB_VRA_V30;
                        break;

                    case FrSignalAspect.FR_RRCLI_A:
                    case FrSignalAspect.FR_RRCLI_ACLI:
                    case FrSignalAspect.FR_RRCLI:
                        KvbVraState = FrKvbVraState.KVB_VRA_V60;
                        break;

                    default:
                        KvbVraState = signalHasSpeedRepeater ? FrKvbVraState.KVB_VRA_AA : FrKvbVraState.None;
                        break;
                }

                switch (SignalAspect)
                {
                    case FrSignalAspect.FR_C_BAL:
                    case FrSignalAspect.FR_C_BAPR:
                    case FrSignalAspect.FR_CV:
                        KvbSigState = FrKvbSigState.KVB_S_C_BAL;
                        break;

                    case FrSignalAspect.FR_C_BM:
                    case FrSignalAspect.FR_S_BM:
                        KvbSigState = FrKvbSigState.KVB_S_S_BM;
                        break;

                    case FrSignalAspect.FR_S_BAL:
                    case FrSignalAspect.FR_S_BAPR:
                    case FrSignalAspect.FR_SCLI:
                    case FrSignalAspect.FR_MCLI:
                    case FrSignalAspect.FR_M:
                        KvbSigState = FrKvbSigState.KVB_S_S_BAL;
                        break;

                    case FrSignalAspect.FR_RR_A:
                    case FrSignalAspect.FR_RRCLI_A:
                    case FrSignalAspect.FR_A:
                        KvbSigState = FrKvbSigState.KVB_S_A;
                        break;

                    case FrSignalAspect.FR_RR_ACLI:
                    case FrSignalAspect.FR_RRCLI_ACLI:
                    case FrSignalAspect.FR_RCLI_ACLI:
                    case FrSignalAspect.FR_ACLI:
                        KvbSigState = FrKvbSigState.KVB_S_ACLI;
                        break;

                    case FrSignalAspect.FR_VLCLI_ANN:
                    case FrSignalAspect.FR_VLCLI_EXE:
                        KvbSigState = FrKvbSigState.KVB_S_VLCLI;
                        break;

                    case FrSignalAspect.FR_RR:
                    case FrSignalAspect.FR_RRCLI:
                    case FrSignalAspect.FR_R:
                    case FrSignalAspect.FR_RCLI:
                    case FrSignalAspect.FR_VL_INF:
                        KvbSigState = FrKvbSigState.KVB_S_VL_INF;
                        break;

                    case FrSignalAspect.FR_VL_SUP:
                        KvbSigState = FrKvbSigState.KVB_S_VL_SUP;
                        break;

                    default:
                        KvbSigState = FrKvbSigState.None;
                        break;
                }
            }
        }

        protected void SerializeAspect()
        {
            TextAspectBuilder.Clear();

            if (SignalAspect != FrSignalAspect.None)
            {
                TextAspectBuilder.Append(SignalAspect.ToString());
            }

            if (CrocodileState != FrCrocodileState.None)
            {
                TextAspectBuilder.Append(CrocodileState.ToString());
            }

            if (KvbVanState != FrKvbVanState.None)
            {
                TextAspectBuilder.Append(KvbVanState.ToString());
            }

            if (KvbVraState != FrKvbVraState.None)
            {
                TextAspectBuilder.Append(KvbVraState.ToString());
            }

            if (KvbSigState != FrKvbSigState.None)
            {
                TextAspectBuilder.Append(KvbSigState.ToString());
            }

            TextSignalAspect = TextAspectBuilder.ToString();
        }
    }
}
