using System;

namespace Nsnbc.Stories.Scenes.Xml
{
    internal class XmlCharacters
    {
        public static ArtName FindArt(string speaker, string pose)
        {
            switch (speaker)
            {
                case "Tišík":
                    return FindTišík(FindPose(pose));
                case "Vědátor":
                    return FindVědátor(FindPose(pose));
                case "Vypravěč":
                    return FindVypravěč(FindPose(pose));
                case "Smíšek":
                    return FindVědátor(FindPose(pose));
                case "Lenka":
                    return FindVypravěč(FindPose(pose));
                case "Háthí":
                    return ArtName.Null;
                default:
                    return ArtName.SlotQuestion;
            }
        }

    

        private static Pose FindPose(string pose)
        {
            switch (pose)
            {
                case "pointing": return Pose.Pointing;
                case "amused": return Pose.Amused;
                case "angry": return Pose.Angry;
                case "thinking": return Pose.Thinking;
                case "excited": return Pose.Excited;
                case "speaking": return Pose.Speaking;
                case "normal": return Pose.Normal;
                case "shrugging": return Pose.Shrugging;
                case "determined": return Pose.Determined;
                case "afraid": return Pose.Afraid;
                case "blushing": return Pose.Blushing;
                default: throw new ArgumentException("Unknown pose '" + pose + "'.");
            }
        }
        
        private static ArtName FindVypravěč(Pose pose)
        {
            switch (pose)
            {
                case Pose.Angry:
                    return ArtName.SkokNastvany;
                case Pose.Pointing:
                    return ArtName.SkokThinking;
                case Pose.Speaking:
                    return ArtName.SkokMluvici;
                case Pose.Afraid:
                    return ArtName.SkokNormalni;
                case Pose.Shrugging:
                    return ArtName.SkokNormalni;
                case Pose.Thinking:
                    return ArtName.SkokThinking;
                case Pose.Determined:
                    return ArtName.SkokNastvany;
                default:
                    return ArtName.SkokMluvici;
            }
        }

        private static ArtName FindVědátor(Pose pose)
        {
            switch (pose)
            {
                case Pose.Angry:
                    return ArtName.VedatorSpeaking;
                case Pose.Pointing:
                    return ArtName.VedatorPointing;
                case Pose.Speaking:
                    return ArtName.VedatorSpeaking;
                case Pose.Afraid:
                    return ArtName.VedatorStrach;
                case Pose.Shrugging:
                    return ArtName.VedatorSpeaking;
                case Pose.Thinking:
                    return ArtName.VedatorThinking;
                case Pose.Determined:
                    return ArtName.VedatorSpeaking;
                default:
                    return ArtName.VedatorSpeaking;
            }
        }
        private static ArtName FindTišík(Pose pose)
        {
            switch (pose)
            {
                case Pose.Angry:
                    return ArtName.TisikAngry;
                case Pose.Pointing:
                    return ArtName.TisikPointing;
                case Pose.Speaking:
                    return ArtName.TisikSpeaking;
                case Pose.Afraid:
                    return ArtName.TisikStrach;
                case Pose.Shrugging:
                    return ArtName.TisikPokrcRamen;
                case Pose.Thinking:
                    return ArtName.TisikThinking;
                case Pose.Determined:
                    return ArtName.TisikDetermined;
                default:
                    return ArtName.TisikSpeaking;
            }
        }
    }

    public enum Pose
    {
        Pointing,
        Amused,
        Angry,
        Thinking,
        Excited,
        Speaking,
        Normal,
        Shrugging,
        Afraid,
        Determined,
        Blushing
    }
}