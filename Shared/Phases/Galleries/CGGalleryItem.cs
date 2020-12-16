using Nsnbc.Auxiliary;
using Nsnbc.Texts;

namespace Nsnbc.Phases.Galleries
{
    internal class CGGalleryItem : GalleryItem
    {
        public CGGalleryItem(GString caption, ArtName art) : base(art, caption, () => Root.PushPhase(new FullImagePhase(art)))
        {
        }
    }
    internal class CGMultipleGalleryItem : GalleryItem
    {
        public CGMultipleGalleryItem(GString caption, params ArtName[] arts) : base(arts[0], caption, () => Root.PushPhase(new FullImagePhase(arts)))
        {
        }
    }
}