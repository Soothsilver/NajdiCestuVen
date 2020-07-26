using Nsnbc.Auxiliary;
using Nsnbc.Core;

namespace Nsnbc.Stories.Scenes
{
    public abstract class Room
    {
        public abstract void Draw(AirSession airSession);
        public abstract ArtName YouAreHere { get; }
    }
}