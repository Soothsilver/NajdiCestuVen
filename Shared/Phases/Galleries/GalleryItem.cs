using System;
using Nsnbc.Texts;

namespace Nsnbc.Phases.Galleries
{
    internal class GalleryItem
    {
        public ArtName Art { get; }
        public GString Caption { get; }
        public Action ClickAction { get; }

        public GalleryItem(ArtName art, GString caption, Action clickAction)
        {
            Art = art;
            Caption = caption;
            ClickAction = clickAction;
        }
    }
}