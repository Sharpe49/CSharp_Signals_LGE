using System;
using System.Collections.Generic;
using System.Linq;
using static Orts.Simulation.Signalling.CsSignalScript;

namespace ORTS.Scripting.Script
{

    public enum TvmType
    {
        None,
        FR_TVM300,
        FR_TVM430,
    }

    public enum TvmSpeedType
    {
        None,
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
        Any,
    }

    public static class TVM300Common
    {
        public static T Min<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) <= 0 ? a : b;
        }

        public static Dictionary<TvmSpeedType, TvmSpeedType> TAB1 = new Dictionary<TvmSpeedType, TvmSpeedType>
        {
            { TvmSpeedType._RRR,   TvmSpeedType._000 },
            { TvmSpeedType._000,  TvmSpeedType._160 },
            { TvmSpeedType._80E,  TvmSpeedType._80 },
            { TvmSpeedType._80,   TvmSpeedType._160 },
            { TvmSpeedType._160E, TvmSpeedType._160 },
            { TvmSpeedType._160,  TvmSpeedType._220 },
            { TvmSpeedType._220E, TvmSpeedType._220 },
            { TvmSpeedType._220,  TvmSpeedType._270 },
            { TvmSpeedType._270V, TvmSpeedType._270 },
            { TvmSpeedType._270,  TvmSpeedType._300 },
            { TvmSpeedType._300V, TvmSpeedType._300 },
            { TvmSpeedType._300,  TvmSpeedType._000 }
        };

        public static Dictionary<TvmSpeedType, TvmSpeedType> TAB2 = new Dictionary<TvmSpeedType, TvmSpeedType>
        {
            { TvmSpeedType._RRR,   TvmSpeedType._000 },
            { TvmSpeedType._000,  TvmSpeedType._000 },
            { TvmSpeedType._80E,  TvmSpeedType._80 },
            { TvmSpeedType._80,   TvmSpeedType._80 },
            { TvmSpeedType._160E, TvmSpeedType._160 },
            { TvmSpeedType._160,  TvmSpeedType._160 },
            { TvmSpeedType._220E, TvmSpeedType._220 },
            { TvmSpeedType._220,  TvmSpeedType._220 },
            { TvmSpeedType._270V, TvmSpeedType._270 },
            { TvmSpeedType._270,  TvmSpeedType._270 },
            { TvmSpeedType._300V, TvmSpeedType._300 },
            { TvmSpeedType._300,  TvmSpeedType._000 }
        };


        public static Dictionary<TvmSpeedType, Aspect> MstsTranslation = new Dictionary<TvmSpeedType, Aspect>
        {
            { TvmSpeedType.None, Aspect.StopAndProceed },
            { TvmSpeedType.Any, Aspect.StopAndProceed },
            { TvmSpeedType._300V, Aspect.Clear_2  },
            { TvmSpeedType._270, Aspect.Clear_1 },
            { TvmSpeedType._270V, Aspect.Approach_3 },
            { TvmSpeedType._220, Aspect.Approach_2 },
            { TvmSpeedType._220E, Aspect.Approach_1 },
            { TvmSpeedType._160, Aspect.Restricting },
            { TvmSpeedType._160E, Aspect.StopAndProceed },
            { TvmSpeedType._80, Aspect.StopAndProceed },
            { TvmSpeedType._80E, Aspect.StopAndProceed },
            { TvmSpeedType._000, Aspect.StopAndProceed },
            { TvmSpeedType._RRR, Aspect.StopAndProceed }
        };

        public static Aspect TVMSpeedTypeToAspect(TvmSpeedType Vc, bool permissive)
        {
            if (!permissive)
            {
                return Aspect.Stop;
            }
            else
            {
                return MstsTranslation[Vc];
            }
        }
    }

    public static class TVM430Common
    {
        public static T Min<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) <= 0 ? a : b;
        }

        public static Dictionary<TvmSpeedType, TvmSpeedType> SNCFV300TAB1 = new Dictionary<TvmSpeedType, TvmSpeedType>
        {
            { TvmSpeedType._RRR,   TvmSpeedType._000 },
            { TvmSpeedType._000,  TvmSpeedType._170 },
            { TvmSpeedType._60E,  TvmSpeedType._60 },
            { TvmSpeedType._60,   TvmSpeedType._170 },
            { TvmSpeedType._80E,  TvmSpeedType._80 },
            { TvmSpeedType._80,   TvmSpeedType._170 },
            { TvmSpeedType._130E, TvmSpeedType._130 },
            { TvmSpeedType._130,  TvmSpeedType._200 },
            { TvmSpeedType._160E, TvmSpeedType._160 },
            { TvmSpeedType._160,  TvmSpeedType._230 },
            { TvmSpeedType._170E, TvmSpeedType._170 },
            { TvmSpeedType._170,  TvmSpeedType._230 },
            { TvmSpeedType._200V, TvmSpeedType._200 },
            { TvmSpeedType._200,  TvmSpeedType._230 },
            { TvmSpeedType._220E, TvmSpeedType._220 },
            { TvmSpeedType._220V, TvmSpeedType._220 },
            { TvmSpeedType._220,  TvmSpeedType._270 },
            { TvmSpeedType._230E, TvmSpeedType._230 },
            { TvmSpeedType._230V, TvmSpeedType._230 },
            { TvmSpeedType._230,  TvmSpeedType._270 },
            { TvmSpeedType._270V, TvmSpeedType._270 },
            { TvmSpeedType._270,  TvmSpeedType._300 },
            { TvmSpeedType._300V, TvmSpeedType._300 },
            { TvmSpeedType._300,  TvmSpeedType._000 }
        };

        public static Dictionary<TvmSpeedType, TvmSpeedType> SNCFV300TAB2 = new Dictionary<TvmSpeedType, TvmSpeedType>
        {
            { TvmSpeedType._RRR,   TvmSpeedType._000 },
            { TvmSpeedType._000,  TvmSpeedType._000 },
            { TvmSpeedType._60E,  TvmSpeedType._60 },
            { TvmSpeedType._60,   TvmSpeedType._60 },
            { TvmSpeedType._80E,  TvmSpeedType._80 },
            { TvmSpeedType._80,   TvmSpeedType._80 },
            { TvmSpeedType._130E, TvmSpeedType._130 },
            { TvmSpeedType._130,  TvmSpeedType._130 },
            { TvmSpeedType._160E, TvmSpeedType._160 },
            { TvmSpeedType._160,  TvmSpeedType._160 },
            { TvmSpeedType._170E, TvmSpeedType._170 },
            { TvmSpeedType._170,  TvmSpeedType._170 },
            { TvmSpeedType._200V, TvmSpeedType._200 },
            { TvmSpeedType._200,  TvmSpeedType._200 },
            { TvmSpeedType._220E, TvmSpeedType._220 },
            { TvmSpeedType._220V, TvmSpeedType._220 },
            { TvmSpeedType._220,  TvmSpeedType._220 },
            { TvmSpeedType._230E, TvmSpeedType._230 },
            { TvmSpeedType._230V, TvmSpeedType._230 },
            { TvmSpeedType._230,  TvmSpeedType._230 },
            { TvmSpeedType._270V, TvmSpeedType._270 },
            { TvmSpeedType._270,  TvmSpeedType._270 },
            { TvmSpeedType._300V, TvmSpeedType._300 },
            { TvmSpeedType._300,  TvmSpeedType._000 }
        };

        public static Dictionary<TvmSpeedType, Aspect> SNCFV300MstsTranslation = new Dictionary<TvmSpeedType, Aspect>
        {
            { TvmSpeedType.None, Aspect.StopAndProceed },
            { TvmSpeedType.Any, Aspect.StopAndProceed  },
            { TvmSpeedType._300V, Aspect.Clear_2 },
            { TvmSpeedType._270, Aspect.Clear_1 },
            { TvmSpeedType._270V, Aspect.Clear_1  },
            { TvmSpeedType._230, Aspect.Approach_3 },
            { TvmSpeedType._230V, Aspect.Approach_3 },
            { TvmSpeedType._230E, Aspect.Approach_3 },
            { TvmSpeedType._220, Aspect.Approach_3 },
            { TvmSpeedType._220V, Aspect.Approach_3 },
            { TvmSpeedType._220E, Aspect.Approach_3 },
            { TvmSpeedType._200, Aspect.Approach_2 },
            { TvmSpeedType._200V, Aspect.Approach_2 },
            { TvmSpeedType._170, Aspect.Approach_2 },
            { TvmSpeedType._170E, Aspect.Approach_2 },
            { TvmSpeedType._160, Aspect.Approach_1 },
            { TvmSpeedType._160E, Aspect.Approach_1 },
            { TvmSpeedType._130, Aspect.Restricting },
            { TvmSpeedType._130E, Aspect.Restricting },
            { TvmSpeedType._80, Aspect.Restricting },
            { TvmSpeedType._80E, Aspect.Restricting },
            { TvmSpeedType._60, Aspect.Restricting },
            { TvmSpeedType._60E, Aspect.Restricting },
            { TvmSpeedType._000, Aspect.Stop },
            { TvmSpeedType._RRR, Aspect.StopAndProceed }
        };

        public static Dictionary<TvmSpeedType, TvmSpeedType> SNCFV320TAB1 = new Dictionary<TvmSpeedType, TvmSpeedType>
        {
            { TvmSpeedType._RRR,   TvmSpeedType._000 },
            { TvmSpeedType._000,  TvmSpeedType._170 },
            { TvmSpeedType._60E,  TvmSpeedType._60 },
            { TvmSpeedType._60,   TvmSpeedType._170 },
            { TvmSpeedType._80E,  TvmSpeedType._80 },
            { TvmSpeedType._80,   TvmSpeedType._170 },
            { TvmSpeedType._130E, TvmSpeedType._130 },
            { TvmSpeedType._130,  TvmSpeedType._200 },
            { TvmSpeedType._160E, TvmSpeedType._160 },
            { TvmSpeedType._160,  TvmSpeedType._230 },
            { TvmSpeedType._170E, TvmSpeedType._170 },
            { TvmSpeedType._170,  TvmSpeedType._230 },
            { TvmSpeedType._200V, TvmSpeedType._200 },
            { TvmSpeedType._200,  TvmSpeedType._230 },
            { TvmSpeedType._220E, TvmSpeedType._220 },
            { TvmSpeedType._220V, TvmSpeedType._220 },
            { TvmSpeedType._220,  TvmSpeedType._270 },
            { TvmSpeedType._230E, TvmSpeedType._230 },
            { TvmSpeedType._230V, TvmSpeedType._230 },
            { TvmSpeedType._230,  TvmSpeedType._270 },
            { TvmSpeedType._270V, TvmSpeedType._270 },
            { TvmSpeedType._270,  TvmSpeedType._300 },
            { TvmSpeedType._300V, TvmSpeedType._300 },
            { TvmSpeedType._300,  TvmSpeedType._320 },
            { TvmSpeedType._320V, TvmSpeedType._320 },
            { TvmSpeedType._320,  TvmSpeedType._000 }
        };

        public static Dictionary<TvmSpeedType, TvmSpeedType> SNCFV320TAB2 = new Dictionary<TvmSpeedType, TvmSpeedType>
        {
            { TvmSpeedType._RRR,   TvmSpeedType._000 },
            { TvmSpeedType._000,  TvmSpeedType._000 },
            { TvmSpeedType._60E,  TvmSpeedType._60 },
            { TvmSpeedType._60,   TvmSpeedType._60 },
            { TvmSpeedType._80E,  TvmSpeedType._80 },
            { TvmSpeedType._80,   TvmSpeedType._80 },
            { TvmSpeedType._130E, TvmSpeedType._130 },
            { TvmSpeedType._130,  TvmSpeedType._130 },
            { TvmSpeedType._160E, TvmSpeedType._160 },
            { TvmSpeedType._160,  TvmSpeedType._160 },
            { TvmSpeedType._170E, TvmSpeedType._170 },
            { TvmSpeedType._170,  TvmSpeedType._170 },
            { TvmSpeedType._200V, TvmSpeedType._200 },
            { TvmSpeedType._200,  TvmSpeedType._200 },
            { TvmSpeedType._220E, TvmSpeedType._220 },
            { TvmSpeedType._220V, TvmSpeedType._220 },
            { TvmSpeedType._220,  TvmSpeedType._220 },
            { TvmSpeedType._230E, TvmSpeedType._230 },
            { TvmSpeedType._230V, TvmSpeedType._230 },
            { TvmSpeedType._230,  TvmSpeedType._230 },
            { TvmSpeedType._270V, TvmSpeedType._270 },
            { TvmSpeedType._270,  TvmSpeedType._270 },
            { TvmSpeedType._300V, TvmSpeedType._300 },
            { TvmSpeedType._300,  TvmSpeedType._300 },
            { TvmSpeedType._320V, TvmSpeedType._320 },
            { TvmSpeedType._320,  TvmSpeedType._000 }
        };

        public static Dictionary<TvmSpeedType, Aspect> SNCFV320MstsTranslation = new Dictionary<TvmSpeedType, Aspect>
        {
            { TvmSpeedType.None, Aspect.StopAndProceed },
            { TvmSpeedType.Any, Aspect.StopAndProceed },
            { TvmSpeedType._320V, Aspect.Clear_2 },
            { TvmSpeedType._300, Aspect.Clear_1  },
            { TvmSpeedType._300V, Aspect.Clear_1  },
            { TvmSpeedType._270, Aspect.Approach_3 },
            { TvmSpeedType._270V, Aspect.Approach_3 },
            { TvmSpeedType._230, Aspect.Approach_2 },
            { TvmSpeedType._230V, Aspect.Approach_2 },
            { TvmSpeedType._230E, Aspect.Approach_2 },
            { TvmSpeedType._220, Aspect.Approach_2 },
            { TvmSpeedType._220E, Aspect.Approach_2 },
            { TvmSpeedType._200, Aspect.Approach_1 },
            { TvmSpeedType._200V, Aspect.Approach_1 },
            { TvmSpeedType._170, Aspect.Approach_1 },
            { TvmSpeedType._170E, Aspect.Approach_1 },
            { TvmSpeedType._160, Aspect.Approach_1 },
            { TvmSpeedType._160E, Aspect.Approach_1 },
            { TvmSpeedType._130, Aspect.Approach_1 },
            { TvmSpeedType._130E, Aspect.Approach_1 },
            { TvmSpeedType._80, Aspect.Restricting },
            { TvmSpeedType._80E, Aspect.Restricting },
            { TvmSpeedType._60, Aspect.Restricting },
            { TvmSpeedType._60E, Aspect.Restricting },
            { TvmSpeedType._000, Aspect.StopAndProceed },
            { TvmSpeedType._RRR, Aspect.StopAndProceed }
        };

        public static Aspect TVMSpeedTypeToAspectV320(TvmSpeedType Vc, bool permissive)
        {
            if (!permissive)
            {
                return Aspect.Stop;
            }
            else
            {
                return SNCFV320MstsTranslation[Vc];
            }
        }
    }
}
