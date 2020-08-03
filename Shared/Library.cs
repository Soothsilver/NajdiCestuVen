using System.Collections.Concurrent;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using Nsnbc.Auxiliary;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.PostSharp;

namespace Nsnbc
{
    public static class Library
    {      
        private static ContentManager content = null!;
        private static readonly ConcurrentDictionary<ArtName, Texture2D> arts = new ConcurrentDictionary<ArtName, Texture2D>();
        private static readonly Dictionary<ArtName, Texture2D> flippedArts = new Dictionary<ArtName, Texture2D>();
        public static Texture2D Pixel { get; private set; } = null!;
        public static Dictionary<string, Texture2D> Icons { get; set; } = new Dictionary<string, Texture2D>();

        [Trace]
        public static void Init(ContentManager incomingContent)
        {
           content = incomingContent;
           Pixel = content.Load<Texture2D>("Art\\Pixel");
           SpriteFont openSans40 = content.Load<SpriteFont>("Fonts\\XnaOpenSans40");
           SpriteFont openSans32 = content.Load<SpriteFont>("Fonts\\XnaOpenSans32");
           SpriteFont openSans24 = content.Load<SpriteFont>("Fonts\\XnaOpenSans24");
           SpriteFont openSans14 = content.Load<SpriteFont>("Fonts\\XnaOpenSans14");
           SpriteFont openSans14Bold = content.Load<SpriteFont>("Fonts\\XnaOpenSans14Bold");
           BitmapFont openSans40e = content.Load<BitmapFont>("Fonts\\ExtendedOpenSans40");
           BitmapFont openSans32e = content.Load<BitmapFont>("Fonts\\ExtendedOpenSans32");
           BitmapFont openSans24e = content.Load<BitmapFont>("Fonts\\ExtendedOpenSans24");
           BitmapFontGroup.MainXna12 = new XnaFontGroup(openSans14, openSans14, openSans14Bold, openSans14Bold);
           BitmapFontGroup.MainXna24 = new XnaFontGroup(openSans24, openSans24, openSans24, openSans24, BitmapFontGroup.MainXna12);
           BitmapFontGroup.MainXna32 = new XnaFontGroup(openSans32, openSans32, openSans32, openSans32, BitmapFontGroup.MainXna24);
           BitmapFontGroup.MainXna40 = new XnaFontGroup(openSans40, openSans40, openSans40, openSans40, BitmapFontGroup.MainXna32);
           BitmapFontGroup.MainExt24 = new ExtendedFontGroup(openSans24e, openSans24e, openSans24e, openSans24e, BitmapFontGroup.MainXna12);
           BitmapFontGroup.MainExt32 = new ExtendedFontGroup(openSans32e, openSans32e, openSans32e, openSans32e, BitmapFontGroup.MainExt24);
           BitmapFontGroup.MainExt40 = new ExtendedFontGroup(openSans40e, openSans40e, openSans40e, openSans40e, BitmapFontGroup.MainExt32);
           BitmapFontGroup.UpdateMainFont();
        }

        public static Texture2D Art(ArtName artName)
        {
            return arts[artName];
        }

        public static Texture2D FlippedArt(ArtName artName)
        {
            if (!flippedArts.ContainsKey(artName))
            {
                flippedArts[artName] = content.Load<Texture2D>("ReverseArt\\" + artName);
            }
            return flippedArts[artName];
        }

        /// <summary>
        /// This method does not work correctly on Android.
        /// </summary>
        [Trace, PublicAPI]
        private static Texture2D Flip(Texture2D art)
        {
            Color[] oldData = new Color[art.Width * art.Height];
            art.GetData(oldData);

            Texture2D newTexture = new Texture2D(Root.Graphics.GraphicsDevice, art.Width, art.Height);
            Color[] newData = new Color[art.Width * art.Height];
         
            // Flipped copy
            for (int column = 0; column < art.Width; column++)
            {
                int flippedColumn = art.Width - column - 1;
                for (int row = 0; row < art.Height; row++)
                {
                    int index = row * art.Width + column;
                    int indexFlipped = row * art.Width + flippedColumn;
                    newData[index] = oldData[indexFlipped];
                }
            }

            newTexture.SetData(newData);
            return newTexture;
        }

        [Trace]
        public static void LoadArt(ArtName art)
        {
            arts.TryAdd(art, content.Load<Texture2D>("Art\\" + art));
        }
    }
}