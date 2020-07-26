using System;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Services;
using Nsnbc.Texts;

namespace Nsnbc.Phases.Galleries
{
    public class SaveLoadGalleryItem : GalleryItem
    {
        private readonly DelayedTexture texture;
        public bool Empty { get; }

        public SaveLoadGalleryItem(DelayedTexture texture, GString caption, Action clickAction, bool empty) : base( caption, clickAction)
        {
            this.texture = texture;
            Empty = empty;
        }

        public override Texture2D Texture => texture.Texture2D;
    }
}