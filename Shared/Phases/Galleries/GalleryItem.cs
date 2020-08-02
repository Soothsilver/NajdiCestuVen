using System;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Texts;

namespace Nsnbc.Phases.Galleries
{
    public class GalleryItem
    {
        public virtual Texture2D Texture { get; }
        public GString Caption { get; }
        public Action ClickAction { get; }

        public GalleryItem(ArtName art, GString caption, Action clickAction)
        {
            Texture = Library.Art(art);
            Caption = caption;
            ClickAction = clickAction;
        }
        protected GalleryItem(GString caption, Action clickAction)
        {
            Caption = caption;
            ClickAction = clickAction;
        }
    }
}