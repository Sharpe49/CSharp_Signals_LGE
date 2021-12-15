using System.Collections.Generic;

namespace ORTS.Scripting.Script
{
    public class ChLVorsignal : ChSignalScript
    {
        public override void Update()
        {
            List<string> nextNormalParts = NextNormalSignalTextAspects;
            List<string> thisNormalParts = TextSignalAspectToList(SignalId, "NORMAL");
            List<string> thisIdentifierParts = TextSignalAspectToList(SignalId, "REPEATER");

            if (thisNormalParts.Contains("CH_IMAGE_H"))
            {
                if (thisIdentifierParts.Contains("CH_SIGNAL_SECTION_DE_LIGNE"))
                {
                    MstsSignalAspect = Aspect.Restricting;
                    TextSignalAspect = "";
                }
                else
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "CH_IMAGE_W";
                }
            }
            else if (thisNormalParts.Contains("CH_IMAGE_6"))
            {
                MstsSignalAspect = Aspect.Restricting;
                TextSignalAspect = "";
            }
            else
            {
                if (AnnounceByImageW(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Stop;
                    TextSignalAspect = "CH_IMAGE_W";
                }
                else if (AnnounceByImage2(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_1;
                    TextSignalAspect = "CH_IMAGE_2*";
                }
                else if (AnnounceByImage3(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_2;
                    TextSignalAspect = "CH_IMAGE_3*";
                }
                else if (AnnounceByImage5(nextNormalParts))
                {
                    MstsSignalAspect = Aspect.Approach_3;
                    TextSignalAspect = "CH_IMAGE_5*";
                }
                else
                {
                    MstsSignalAspect = Aspect.Clear_2;
                    TextSignalAspect = "CH_IMAGE_1*";
                }
            }

            if (thisNormalParts.Count <= 0)
            {
                TextSignalAspect += SwissTCS("", TextSignalAspect);
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}