using System;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Texts;

namespace Nsnbc.Phases.Galleries
{
    public class SaveLoadGalleryItem : GalleryItem
    {
        public bool Empty { get; }

        public SaveLoadGalleryItem(Texture2D texture, GString caption, Action clickAction, bool empty) : base(texture, caption, clickAction)
        {
            Empty = empty;
        }
    }
}