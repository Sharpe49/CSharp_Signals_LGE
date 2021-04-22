using System;
using System.Collections.Generic;
using static Orts.Simulation.Signalling.CsSignalScript;

namespace ORTS.Scripting.Script
{
    public enum TVMSpeedType
    {
        _RRR,
        _000,
        _30E,
        _30,
        _60E,
        _60,
        _80E,
        _80,
        _100E,
        _100,
        _130E,
        _130,
        _160E,
        _160,
        _170E,
        _170,
        _200V,
        _200,
        _220E,
        _220V,
        _220,
        _230E,
        _230V,
        _230,
        _270V,
        _270,
        _300V,
        _300,
        _320V,
        _320,
        Any
    }

    public static class TVM430Common
    {
        public static T Min<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) <= 0 ? a : b;
        }

        public static Dictionary<TVMSpeedType, TVMSpeedType> SNCFV300TAB1 = new Dictionary<TVMSpeedType, TVMSpeedType>
        {
            { TVMSpeedType._RRR,   TVMSpeedType._000 },
            { TVMSpeedType._000,  TVMSpeedType._170 },
            { TVMSpeedType._60E,  TVMSpeedType._60 },
            { TVMSpeedType._60,   TVMSpeedType._170 },
            { TVMSpeedType._80E,  TVMSpeedType._80 },
            { TVMSpeedType._80,   TVMSpeedType._170 },
            { TVMSpeedType._130E, TVMSpeedType._130 },
            { TVMSpeedType._130,  TVMSpeedType._200 },
            { TVMSpeedType._160E, TVMSpeedType._160 },
            { TVMSpeedType._160,  TVMSpeedType._230 },
            { TVMSpeedType._170E, TVMSpeedType._170 },
            { TVMSpeedType._170,  TVMSpeedType._230 },
            { TVMSpeedType._200V, TVMSpeedType._200 },
            { TVMSpeedType._200,  TVMSpeedType._230 },
            { TVMSpeedType._220E, TVMSpeedType._220 },
            { TVMSpeedType._220V, TVMSpeedType._220 },
            { TVMSpeedType._220,  TVMSpeedType._270 },
            { TVMSpeedType._230E, TVMSpeedType._230 },
            { TVMSpeedType._230V, TVMSpeedType._230 },
            { TVMSpeedType._230,  TVMSpeedType._270 },
            { TVMSpeedType._270V, TVMSpeedType._270 },
            { TVMSpeedType._270,  TVMSpeedType._300 },
            { TVMSpeedType._300V, TVMSpeedType._300 },
            { TVMSpeedType._300,  TVMSpeedType._000 }
        };

        public static Dictionary<TVMSpeedType, TVMSpeedType> SNCFV300TAB2 = new Dictionary<TVMSpeedType, TVMSpeedType>
        {
            { TVMSpeedType._RRR,   TVMSpeedType._000 },
            { TVMSpeedType._000,  TVMSpeedType._000 },
            { TVMSpeedType._60E,  TVMSpeedType._60 },
            { TVMSpeedType._60,   TVMSpeedType._60 },
            { TVMSpeedType._80E,  TVMSpeedType._80 },
            { TVMSpeedType._80,   TVMSpeedType._80 },
            { TVMSpeedType._130E, TVMSpeedType._130 },
            { TVMSpeedType._130,  TVMSpeedType._130 },
            { TVMSpeedType._160E, TVMSpeedType._160 },
            { TVMSpeedType._160,  TVMSpeedType._160 },
            { TVMSpeedType._170E, TVMSpeedType._170 },
            { TVMSpeedType._170,  TVMSpeedType._170 },
            { TVMSpeedType._200V, TVMSpeedType._200 },
            { TVMSpeedType._200,  TVMSpeedType._200 },
            { TVMSpeedType._220E, TVMSpeedType._220 },
            { TVMSpeedType._220V, TVMSpeedType._220 },
            { TVMSpeedType._220,  TVMSpeedType._220 },
            { TVMSpeedType._230E, TVMSpeedType._230 },
            { TVMSpeedType._230V, TVMSpeedType._230 },
            { TVMSpeedType._230,  TVMSpeedType._230 },
            { TVMSpeedType._270V, TVMSpeedType._270 },
            { TVMSpeedType._270,  TVMSpeedType._270 },
            { TVMSpeedType._300V, TVMSpeedType._300 },
            { TVMSpeedType._300,  TVMSpeedType._000 }
        };

        public static Dictionary<TVMSpeedType, Aspect> SNCFV300MstsTranslation = new Dictionary<TVMSpeedType, Aspect>
        {
            { TVMSpeedType.Any, Aspect.StopAndProceed  },
            { TVMSpeedType._300V, Aspect.Clear_2 },
            { TVMSpeedType._270, Aspect.Clear_1 },
            { TVMSpeedType._270V, Aspect.Clear_1  },
            { TVMSpeedType._230, Aspect.Approach_3 },
            { TVMSpeedType._230V, Aspect.Approach_3 },
            { TVMSpeedType._230E, Aspect.Approach_3 },
            { TVMSpeedType._220, Aspect.Approach_3 },
            { TVMSpeedType._220V, Aspect.Approach_3 },
            { TVMSpeedType._220E, Aspect.Approach_3 },
            { TVMSpeedType._200, Aspect.Approach_2 },
            { TVMSpeedType._200V, Aspect.Approach_2 },
            { TVMSpeedType._170, Aspect.Approach_2 },
            { TVMSpeedType._170E, Aspect.Approach_2 },
            { TVMSpeedType._160, Aspect.Approach_1 },
            { TVMSpeedType._160E, Aspect.Approach_1 },
            { TVMSpeedType._130, Aspect.Restricting },
            { TVMSpeedType._130E, Aspect.Restricting },
            { TVMSpeedType._80, Aspect.Restricting },
            { TVMSpeedType._80E, Aspect.Restricting },
            { TVMSpeedType._60, Aspect.Restricting },
            { TVMSpeedType._60E, Aspect.Restricting },
            { TVMSpeedType._000, Aspect.Stop },
            { TVMSpeedType._RRR, Aspect.StopAndProceed }
        };

        public static Dictionary<TVMSpeedType, TVMSpeedType> SNCFV320TAB1 = new Dictionary<TVMSpeedType, TVMSpeedType>
        {
            { TVMSpeedType._RRR,   TVMSpeedType._000 },
            { TVMSpeedType._000,  TVMSpeedType._170 },
            { TVMSpeedType._60E,  TVMSpeedType._60 },
            { TVMSpeedType._60,   TVMSpeedType._170 },
            { TVMSpeedType._80E,  TVMSpeedType._80 },
            { TVMSpeedType._80,   TVMSpeedType._170 },
            { TVMSpeedType._130E, TVMSpeedType._130 },
            { TVMSpeedType._130,  TVMSpeedType._200 },
            { TVMSpeedType._160E, TVMSpeedType._160 },
            { TVMSpeedType._160,  TVMSpeedType._230 },
            { TVMSpeedType._170E, TVMSpeedType._170 },
            { TVMSpeedType._170,  TVMSpeedType._230 },
            { TVMSpeedType._200V, TVMSpeedType._200 },
            { TVMSpeedType._200,  TVMSpeedType._230 },
            { TVMSpeedType._220E, TVMSpeedType._220 },
            { TVMSpeedType._220V, TVMSpeedType._220 },
            { TVMSpeedType._220,  TVMSpeedType._270 },
            { TVMSpeedType._230E, TVMSpeedType._230 },
            { TVMSpeedType._230V, TVMSpeedType._230 },
            { TVMSpeedType._230,  TVMSpeedType._270 },
            { TVMSpeedType._270V, TVMSpeedType._270 },
            { TVMSpeedType._270,  TVMSpeedType._300 },
            { TVMSpeedType._300V, TVMSpeedType._300 },
            { TVMSpeedType._300,  TVMSpeedType._320 },
            { TVMSpeedType._320V, TVMSpeedType._320 },
            { TVMSpeedType._320,  TVMSpeedType._000 }
        };

        public static Dictionary<TVMSpeedType, TVMSpeedType> SNCFV320TAB2 = new Dictionary<TVMSpeedType, TVMSpeedType>
        {
            { TVMSpeedType._RRR,   TVMSpeedType._000 },
            { TVMSpeedType._000,  TVMSpeedType._000 },
            { TVMSpeedType._60E,  TVMSpeedType._60 },
            { TVMSpeedType._60,   TVMSpeedType._60 },
            { TVMSpeedType._80E,  TVMSpeedType._80 },
            { TVMSpeedType._80,   TVMSpeedType._80 },
            { TVMSpeedType._130E, TVMSpeedType._130 },
            { TVMSpeedType._130,  TVMSpeedType._130 },
            { TVMSpeedType._160E, TVMSpeedType._160 },
            { TVMSpeedType._160,  TVMSpeedType._160 },
            { TVMSpeedType._170E, TVMSpeedType._170 },
            { TVMSpeedType._170,  TVMSpeedType._170 },
            { TVMSpeedType._200V, TVMSpeedType._200 },
            { TVMSpeedType._200,  TVMSpeedType._200 },
            { TVMSpeedType._220E, TVMSpeedType._220 },
            { TVMSpeedType._220V, TVMSpeedType._220 },
            { TVMSpeedType._220,  TVMSpeedType._220 },
            { TVMSpeedType._230E, TVMSpeedType._230 },
            { TVMSpeedType._230V, TVMSpeedType._230 },
            { TVMSpeedType._230,  TVMSpeedType._230 },
            { TVMSpeedType._270V, TVMSpeedType._270 },
            { TVMSpeedType._270,  TVMSpeedType._270 },
            { TVMSpeedType._300V, TVMSpeedType._300 },
            { TVMSpeedType._300,  TVMSpeedType._300 },
            { TVMSpeedType._320V, TVMSpeedType._320 },
            { TVMSpeedType._320,  TVMSpeedType._000 }
        };

        public static Dictionary<TVMSpeedType, Aspect> SNCFV320MstsTranslation = new Dictionary<TVMSpeedType, Aspect>
        {
            { TVMSpeedType.Any, Aspect.StopAndProceed },
            { TVMSpeedType._320V, Aspect.Clear_2 },
            { TVMSpeedType._300, Aspect.Clear_1  },
            { TVMSpeedType._300V, Aspect.Clear_1  },
            { TVMSpeedType._270, Aspect.Approach_3 },
            { TVMSpeedType._270V, Aspect.Approach_3 },
            { TVMSpeedType._230, Aspect.Approach_2 },
            { TVMSpeedType._230V, Aspect.Approach_2 },
            { TVMSpeedType._230E, Aspect.Approach_2 },
            { TVMSpeedType._220, Aspect.Approach_2 },
            { TVMSpeedType._220E, Aspect.Approach_2 },
            { TVMSpeedType._200, Aspect.Approach_1 },
            { TVMSpeedType._200V, Aspect.Approach_1 },
            { TVMSpeedType._170, Aspect.Approach_1 },
            { TVMSpeedType._170E, Aspect.Approach_1 },
            { TVMSpeedType._160, Aspect.Approach_1 },
            { TVMSpeedType._160E, Aspect.Approach_1 },
            { TVMSpeedType._130, Aspect.Approach_1 },
            { TVMSpeedType._130E, Aspect.Approach_1 },
            { TVMSpeedType._80, Aspect.Restricting },
            { TVMSpeedType._80E, Aspect.Restricting },
            { TVMSpeedType._60, Aspect.Restricting },
            { TVMSpeedType._60E, Aspect.Restricting },
            { TVMSpeedType._000, Aspect.Stop },
            { TVMSpeedType._RRR, Aspect.StopAndProceed }
        };
    }
}
