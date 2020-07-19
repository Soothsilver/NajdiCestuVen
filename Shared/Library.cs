using System.Collections.Generic;
using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.PostSharp;

namespace Nsnbc
{
    public static class Library
    {      
        private static ContentManager content = null!;
        private static Dictionary<ArtName, Texture2D> arts = new Dictionary<ArtName, Texture2D>();
        private static Dictionary<ArtName, Texture2D> flippedArts = new Dictionary<ArtName, Texture2D>();
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
           BitmapFontGroup.Main24 = new BitmapFontGroup(openSans24, openSans24, openSans24, openSans24, null);
           BitmapFontGroup.Main32 = new BitmapFontGroup(openSans32, openSans32, openSans32, openSans32, BitmapFontGroup.Main24);
           BitmapFontGroup.Main40 = new BitmapFontGroup(openSans40, openSans40, openSans40, openSans40, BitmapFontGroup.Main32);
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
        [Trace]
        private static Texture2D Flip(Texture2D art)
        {
            var primaryTexture = art;
            Color[] oldData = new Color[primaryTexture.Width * primaryTexture.Height];
            primaryTexture.GetData(oldData);

            Texture2D newTexture = new Texture2D(Root.Graphics.GraphicsDevice, primaryTexture.Width, primaryTexture.Height);
            Color[] newData = new Color[primaryTexture.Width * primaryTexture.Height];


         
                // Flipped copy
                for (int column = 0; column < primaryTexture.Width; column++)
                {
                    int flippedColumn = primaryTexture.Width - column - 1;
                    for (int row = 0; row < primaryTexture.Height; row++)
                    {
                        int index = row * primaryTexture.Width + column;
                        int indexFlipped = row * primaryTexture.Width + flippedColumn;
                        newData[index] = oldData[indexFlipped];
                    }
                }
         

            newTexture.SetData(newData);
            return newTexture;
        }

        [Trace]
        public static void LoadArt(ArtName art)
        {
            arts.Add(art, content.Load<Texture2D>("Art\\" + art));
        }
    }
}