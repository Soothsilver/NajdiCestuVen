using System;
using Nsnbc.Core;

namespace Nsnbc.Stories.Scenes.Xml
{
    internal class XmlCharacters
    {
        public static ArtName FindArt(string speaker, string? pose)
        {
            if (pose == null)
            {
                return ArtName.Null;
            }

            Pose truePose = FindPose(pose);
            return FindArt(speaker, truePose);
        }

        public static ArtName FindArt(string speaker, Pose pose)
        {
            switch (speaker)
            {
                case "Tišík":
                    return FindTišík(pose);
                case "Vědátor":
                    return FindVědátor(pose);
                case "Vypravěč":
                    return FindVypravěč(pose);
                case "Smíšek":
                    return FindSmíšek(pose);
                case "Lenka":
                    return FindLenka(pose);
                case "Háthí":
                    return ArtName.Null;
                case "Akela":
                    return FindAkela(pose);
                case "Vlčata a světlušky":
                    return ArtName.TripleHappy;
                default:
                    return ArtName.SlotQuestion;
            }
        }

        private static ArtName FindAkela(Pose pose)
        {
            switch (pose)
            {
                case Pose.Excited:
                    return ArtName.AkelaExcited;
                case Pose.Speaking:
                    return ArtName.AkelaExplaining;
                case Pose.Normal:
                    return ArtName.AkelaNormal;
                case Pose.Thinking:
                    return ArtName.AkelaThinking;
                default:
                    return ArtName.AkelaExplaining;
            }
        }

        private static ArtName FindSmíšek(Pose pose)
        {
            switch (pose)
            {
                case Pose.Afraid:
                    return ArtName.SmisekAfraid;
                case Pose.Angry:
                    return ArtName.SmisekAngry;
                case Pose.Pointing:
                    return ArtName.SmisekPointing;
                case Pose.Speaking:
                    return ArtName.SmisekNormal;
                case Pose.Excited:
                    return ArtName.SmisekExcited;
                case Pose.Determined:
                    return ArtName.SmisekDetermined;
                case Pose.Thinking:
                    return ArtName.SmisekThinking;
                case Pose.Shrugging:
                case Pose.Amused:
                case Pose.Blushing:
                default:
                    return ArtName.SmisekNormal;
            }
        }
        private static ArtName FindLenka(Pose pose)
        {
            switch (pose)
            {
                case Pose.Afraid:
                    return ArtName.LenkaAfraid;
                case Pose.Angry:
                    return ArtName.LenkaAngry;
                case Pose.Speaking:
                    return ArtName.LenkaSpeaking;
                case Pose.Excited:
                    return ArtName.LenkaExcited;
                case Pose.Determined:
                    return ArtName.LenkaDetermined;
                case Pose.Thinking:
                    return ArtName.LenkaThinking;
                case Pose.Amused:
                    return ArtName.LenkaAmused;
                case Pose.Blushing:
                    return ArtName.LenkaBlushing;
                default:
                case Pose.Pointing:
                case Pose.Shrugging:
                    return ArtName.LenkaSpeaking;
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
                case Pose.Excited:
                    return ArtName.TisikExcited;
                default:
                    return ArtName.TisikSpeaking;
            }
        }
    }
}