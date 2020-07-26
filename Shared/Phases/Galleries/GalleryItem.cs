using System;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Texts;

namespace Nsnbc.Phases.Galleries
{
    public class GalleryItem
    {
        public Texture2D Texture { get; }
        public GString Caption { get; }
        public Action ClickAction { get; }

        public GalleryItem(ArtName art, GString caption, Action clickAction)
        {
            Texture = Library.Art(art);
            Caption = caption;
            ClickAction = clickAction;
        }
        public GalleryItem(Texture2D texture, GString caption, Action clickAction)
        {
            Texture = texture;
            Caption = caption;
            ClickAction = clickAction;
        }
    }
}