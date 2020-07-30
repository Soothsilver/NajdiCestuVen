using PostSharp.Community.ToString;

namespace Nsnbc.Stories.Scenes.Prison
{
    public abstract class PrisonRoom : Room
    {
        [IgnoreDuringToString]
        public new PrisonScene Parent
        {
            get { return (PrisonScene) base.Parent; }
            set { base.Parent = value; }
        }
    }
}