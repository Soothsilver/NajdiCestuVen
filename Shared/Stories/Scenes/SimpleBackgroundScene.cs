using JetBrains.Annotations;
using Nsnbc.Auxiliary;
using Nsnbc.Core;

namespace Nsnbc.Stories.Scenes
{
    public class SimpleBackgroundScene : Scene
    {
        [PublicAPI]
        public ArtName Art { get; }

        public SimpleBackgroundScene(ArtName art)
        {
            Art = art;
        }
        
        public override void Draw(AirSession airSession)
        {
            Primitives.DrawZoomed(Library.Art(Art), CurrentZoom);
        }
    }
}