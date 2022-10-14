using System;

namespace ORTS.Scripting.Script
{
    public abstract class LuSignalScript : SignalScript
    {
        private readonly TextAspectBuilder TextAspectBuilder = new TextAspectBuilder();

        private class SignalState : IEquatable<SignalState>
        {
            public SignalAspect SignalAspect { get; set; } = SignalAspect.None;
            public SignalAspect SecondSignalAspect { get; set; } = SignalAspect.None;
            public CrocodileState CrocodileState { get; set; } = CrocodileState.None;

            public bool Equals(SignalState other)
            {
                return SignalAspect == other.SignalAspect
                       && SecondSignalAspect == other.SecondSignalAspect
                       && CrocodileState == other.CrocodileState;
            }

            public void Copy(SignalState other)
            {
                SignalAspect = other.SignalAspect;
                SecondSignalAspect = other.SecondSignalAspect;
                CrocodileState = other.CrocodileState;
            }
        }

        public SignalAspect SignalAspect
        {
            get => State.SignalAspect;
            set => State.SignalAspect = value;
        }

        public SignalAspect SecondSignalAspect
        {
            get => State.SecondSignalAspect;
            set => State.SecondSignalAspect = value;
        }

        public CrocodileState CrocodileState
        {
            get => State.CrocodileState;
            set => State.CrocodileState = value;
        }

        private readonly SignalState State = new SignalState();
        private readonly SignalState PreviousState = new SignalState();

        protected void LuxembourgishTCS()
        {
            LuxembourgishCrocodile();
        }

        protected void LuxembourgishCrocodile()
        {
            switch (SignalAspect)
            {
                case SignalAspect.LU_SFP1:
                case SignalAspect.LU_SFAv1:
                case SignalAspect.LU_SFAv3:
                    CrocodileState = CrocodileState.CROCODILE_SF;
                    break;

                default:
                    CrocodileState = CrocodileState.CROCODILE_SO;
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

            if (SecondSignalAspect != SignalAspect.None)
            {
                TextAspectBuilder.Append(SecondSignalAspect.ToString());
            }

            if (CrocodileState != CrocodileState.None)
            {
                TextAspectBuilder.Append(CrocodileState.ToString());
            }

            TextSignalAspect = TextAspectBuilder.ToString();

            PreviousState.Copy(State);
        }
    }
}
