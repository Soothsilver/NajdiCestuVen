using Nsnbc.Texts;

namespace Nsnbc.Core
{
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

    internal static class PoseExtensions
    {
        public static GString ToGString(this Pose pose)
        {
            switch (pose)
            {
                case Pose.Afraid: return G.T("Strach");
                case Pose.Amused: return G.T("Úšklebek");
                case Pose.Angry: return G.T("Hněv");
                case Pose.Thinking: return G.T("Přemýšlení");
                case Pose.Pointing: return G.T("Ukazuje");
                case Pose.Excited: return G.T("Nadšení");
                case Pose.Speaking: return G.T("Mluví");
                case Pose.Normal: return G.T("Normální");
                case Pose.Shrugging: return G.T("Pokrčení ramen");
                case Pose.Determined: return G.T("Odhodlání");
                case Pose.Blushing: return G.T("Ruměnec");
                default: return G.T(pose.ToString());
            }
        }
    }
}