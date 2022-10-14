using Orts.Simulation.Signalling;
using ORTS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORTS.Scripting.Script
{
    public enum SignalAspect
    {
        None,
        EOA,

        FR_FSO,
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
        FR_OEILLETON_ALLUME,
        FR_OEILLETON_ETEINT,
        FR_TABLEAU_G_D,
        FR_TABLEAU_G_D_PRESENTE,
        FR_TABLEAU_G_D_EFFACE,
        FR_TIVR_ETEINT,
        FR_TIVR_PRESENTE,
        FR_TIVR_EFFACE,
        FR_TIVD_PRESENTE,
        FR_TIVD_EFFACE,
        FR_TABP_PRESENTE,
        FR_TABP_EFFACE,
        FR_REPRISE_VL,
        FR_BJ_PRESENTEE,
        FR_BJ_EFFACEE,
        FR_ID_1_FEU,
        FR_ID_2_FEUX,
        FR_ID_3_FEUX,
        FR_ID_4_FEUX,
        FR_ID_5_FEUX,
        FR_ID_ETEINT,
        FR_TIDD_GAUCHE,
        FR_TIDD_DROITE,
        FR_TIDD_ETEINT,
        FR_TLD_1,
        FR_TLD_2,
        FR_TLD_3,
        FR_TLD_4,
        FR_TLD_5,
        FR_TLD_6,
        FR_TLD_7,
        FR_TLD_8,
        FR_TLD_9,
        FR_TLD_EFFACE,
        FR_TIP_1,
        FR_TIP_2,
        FR_TIP_3,
        FR_TIP_4,
        FR_TIP_5,
        FR_TIP_6,
        FR_TIP_ETEINT,
        FR_TLC_DAMIER,
        FR_TLC_T,
        FR_TLC_SLD,
        FR_TECS_PRESENTE,
        FR_TECS_EFFACE,
        FR_TSCS_PRESENTE,
        FR_TSCS_EFFACE,
        FR_TECS_TSCS_EFFACE,
        FR_BP_ANNONCE_ETEINT,
        FR_BP_ANNONCE_PRESENTE,
        FR_BP_ANNONCE_EFFACE,
        FR_BP_EXECUTION_PRESENTE,
        FR_BP_FP_1500V_PRESENTE,
        FR_BP_FP_3000V_PRESENTE,
        FR_BP_FP_25000V_PRESENTE,
        FR_BP_FP_25000VLGV_PRESENTE,
        FR_BP_FP_25000VET_PRESENTE,
        FR_CCT_ANNONCE_PRESENTE,
        FR_CCT_EXECUTION_PRESENTE,
        FR_CCT_FP_PRESENTE,

        TVM430_FEU_BLANC_ALLUME,
        TVM430_FEU_BLANC_ETEINT,

        LU_SFP1,
        LU_SFP2,
        LU_SFP3,
        LU_SFAv1,
        LU_SFAv2,
        LU_SFAv3,
        LU_SFVb1,
        LU_SFVb2,
        LU_SFVo_PRESENTE,
        LU_SFVo_EFFACE,
        LU_SFCCI_O_PRESENTE,
        LU_SFCCI_O_EFFACE,
        LU_SFCCI_TF_PRESENTE,
        LU_SFCCI_TF_EFFACE,
        LU_SFI_PRESENTE,
        LU_SFI_EFFACE,
        LU_SFAvI_PRESENTE,
        LU_SFAvI_EFFACE,
        LU_SFAvVo_PRESENTE,
        LU_SFAvVo_EFFACE,

        CH_IMAGE_H,
        CH_IMAGE_1,
        CH_IMAGE_2,
        CH_IMAGE_3,
        CH_IMAGE_5,
        CH_IMAGE_6,
        CH_IMAGE_W,
        CH_IMAGE_1A,
        CH_IMAGE_2A,
        CH_IMAGE_3A,
        CH_IMAGE_5A,
        CH_NAIN_FERME,
        CH_NAIN_OUVERT,
        CH_AUTORISATION_DE_DEPART_PRESENTEE,
        CH_AUTORISATION_DE_DEPART_EFFACEE,
    }

    public enum DirectionInfoAspect
    {
        None,
        DIR0,
        DIR1,
        DIR2,
        DIR3,
        DIR4,
        DIR5,
        DIR6,
        DIR7,
        GROUPE0,
        GROUPE1,
        BJ_VOIE_OCCUPEE,
        BJ_VOIE_LIBRE,
    }

    public enum SpeedInfoAspect
    {
        None,
        FR_VITESSE_AIGUILLE_30,
        FR_VITESSE_AIGUILLE_40,
        FR_VITESSE_AIGUILLE_50,
        FR_VITESSE_AIGUILLE_60,
        FR_VITESSE_AIGUILLE_70,
        FR_VITESSE_AIGUILLE_80,
        FR_VITESSE_AIGUILLE_90,
        FR_VITESSE_AIGUILLE_100,
        FR_VITESSE_AIGUILLE_110,
        FR_VITESSE_AIGUILLE_120,
        FR_VITESSE_AIGUILLE_130,
        FR_VITESSE_AIGUILLE_140,
        FR_VITESSE_AIGUILLE_150,
        FR_VITESSE_AIGUILLE_160,
        FR_VITESSE_AIGUILLE_NON_PARAMETREE,
    }

    public enum IpcsInfoAspect
    {
        None,
        FR_IPCS_ENTREE_CONTRE_SENS,
        FR_IPCS_SORTIE_CONTRE_SENS,
        FR_IPCS_NON_PARAMETRE,
    }

    public sealed class TextAspectBuilder
    {
        private readonly StringBuilder StringBuilder = new StringBuilder();

        public TextAspectBuilder Append(string value)
        {
            if (StringBuilder.Length > 0)
            {
                StringBuilder.Append(" ");
            }
            StringBuilder.Append(value);

            return this;
        }

        public TextAspectBuilder Append(string format, object arg0)
        {
            if (StringBuilder.Length > 0)
            {
                StringBuilder.Append(" ");
            }
            StringBuilder.AppendFormat(format, arg0);

            return this;
        }

        public TextAspectBuilder Clear()
        {
            StringBuilder.Clear();

            return this;
        }

        public override string ToString() => StringBuilder.ToString();
    }

    public sealed class SignalInfo
    {
        public SignalAspect Aspect = SignalAspect.None;
        public DirectionInfoAspect DirectionInfoAspect = DirectionInfoAspect.None;
        public SpeedInfoAspect SpeedInfoAspect = SpeedInfoAspect.None;
        public IpcsInfoAspect IpcsInfoAspect = IpcsInfoAspect.None;
        public ChInfoAspect ChInfoAspect = ChInfoAspect.None;
        public TvmType TvmType = TvmType.None;
        public TvmSpeedType Ve = TvmSpeedType.None;
        public TvmSpeedType Vc = TvmSpeedType.None;
        public TvmSpeedType Va = TvmSpeedType.None;

        public bool ESUBO = false;
    }

    public abstract class SignalScript : CsSignalScript
    {
        public SignalInfo NextNormalSignalInfo => DeserializeAspect(NextSignalIdWithoutTextAspect("FR_REPRISE_VL", "NORMAL"), "NORMAL");

        public List<int> SignalsWithResume = new List<int>();

        private readonly Dictionary<(int SignalId, string SignalType), (string Text, SignalInfo Info)> Cache = new Dictionary<(int SignalId, string SignalType), (string Text, SignalInfo info)>();

        public override void Initialize()
        {
        }

        public override void Update()
        {
        }

        public SignalInfo FindSignalAspect(string findText, string signalType, int maxSignals)
        {
            string text = string.Empty;
            int signalId = -1;

            for (int i = 0; i < maxSignals; i++)
            {
                signalId = NextSignalId(signalType, i);

                if (signalId >= 0)
                {
                    string aspect = IdTextSignalAspect(signalId, signalType);

                    if (aspect.Contains(findText))
                    {
                        text = aspect;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            if (signalId < 0)
            {
                return new SignalInfo();
            }

            return DeserializeAspect(signalId, signalType);
        }

        public int NextSignalIdWithoutTextAspect(string text, string sigfn, int count = 0)
        {
            int id = NextSignalId(sigfn, count);
            if (id < 0)
            {
                return -1;
            }
            else
            {
                string textAspect = IdTextSignalAspect(id, sigfn);

                if (textAspect.Contains(text))
                {
                    if (text == "FR_REPRISE_VL")
                    {
                        SignalsWithResume.Add(id);
                    }

                    return NextSignalIdWithoutTextAspect(text, sigfn, count + 1);
                }
                else
                {
                    return id;
                }
            }
        }

        private List<string> TextSignalAspectToList(int signalId, string signalType)
        {
            if (signalId < 0)
            {
                return (signalType == "NORMAL" ? new List<string>() { "EOA" } : new List<string>());
            }
            else
            {
                string textAspect = IdTextSignalAspect(signalId, signalType);
                List<string> list = new List<string>();
                int i = 0;

                while (textAspect.Length > 0 && i < 5)
                {
                    list = list.Concat(textAspect.Split(' ')).ToList();

                    i++;
                    textAspect = IdTextSignalAspect(signalId, signalType, i);
                }

                return list.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
            }
        }

        public SignalInfo DeserializeAspect(int signalId, string signalType)
        {
            if (signalId < 0)
            {
                SignalInfo signalInfo = new SignalInfo();

                if (signalType == "NORMAL")
                {
                    signalInfo.Aspect = SignalAspect.EOA;
                }

                return signalInfo;
            }
            else
            {
                string textAspect = IdTextSignalAspect(signalId, signalType);

                var cachedAspect = Cache.FirstOrDefault(kv => kv.Key.SignalId == signalId && kv.Key.SignalType == signalType).Value;

                if (cachedAspect.Text == null || cachedAspect.Text != textAspect)
                {
                    SignalInfo info = DeserializeAspect(TextSignalAspectToList(signalId, signalType));

                    Cache[(signalId, signalType)] = (textAspect, info);

                    return info;
                }
                else
                {
                    return cachedAspect.Info;
                }
            }
        }

        public SignalInfo DeserializeAspect(List<string> textAspectList) 
        {
            SignalInfo signalInfo = new SignalInfo();

            foreach (string part in textAspectList)
            {
                if (signalInfo.Aspect == SignalAspect.None)
                {
                    if (Enum.TryParse(part, out signalInfo.Aspect))
                    {
                        continue;
                    }
                }

                if (signalInfo.DirectionInfoAspect == DirectionInfoAspect.None)
                {
                    if (Enum.TryParse(part, out signalInfo.DirectionInfoAspect))
                    {
                        continue;
                    }
                }

                if (signalInfo.SpeedInfoAspect == SpeedInfoAspect.None)
                {
                    if (Enum.TryParse(part, out signalInfo.SpeedInfoAspect))
                    {
                        continue;
                    }
                }

                if (signalInfo.IpcsInfoAspect == IpcsInfoAspect.None)
                {
                    if (Enum.TryParse(part, out signalInfo.IpcsInfoAspect))
                    {
                        continue;
                    }
                }

                if (signalInfo.ChInfoAspect == ChInfoAspect.None)
                {
                    if (Enum.TryParse(part, out signalInfo.ChInfoAspect))
                    {
                        continue;
                    }
                }

                if (signalInfo.TvmType == TvmType.None)
                {
                    if (Enum.TryParse(part, out signalInfo.TvmType))
                    {
                        continue;
                    }
                }

                if (part.StartsWith("Ve"))
                {
                    signalInfo.Ve = (TvmSpeedType)Enum.Parse(typeof(TvmSpeedType), "_" + part.Substring(2));
                    continue;
                }
                else if (part.StartsWith("Vc"))
                {
                    signalInfo.Vc = (TvmSpeedType)Enum.Parse(typeof(TvmSpeedType), "_" + part.Substring(2));
                    continue;
                }
                else if (part.StartsWith("Va"))
                {
                    signalInfo.Va = (TvmSpeedType)Enum.Parse(typeof(TvmSpeedType), "_" + part.Substring(2));
                    continue;
                }

                if (part == "ESUBO")
                {
                    signalInfo.ESUBO = true;
                }
            }

            if (signalInfo.TvmType != TvmType.None && signalInfo.Va == TvmSpeedType.None)
            {
                signalInfo.Va = TvmSpeedType.Any;
            }

            return signalInfo;
        }

        public override void HandleEvent(SignalEvent evt, string message = "")
        {
            Update();
        }

        public void SetSpeedLimitKpH(float passengerSpeedLimitKpH, float freightSpeedLimitKpH, bool asap, bool reset, bool noSpeedReduction, bool isWarning)
        {
            SetSpeedLimit(MpS.FromKpH(passengerSpeedLimitKpH), MpS.FromKpH(freightSpeedLimitKpH), asap, reset, noSpeedReduction, isWarning);
        }
    }
}
