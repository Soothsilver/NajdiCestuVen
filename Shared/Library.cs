using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Android;
using Nsnbc.Auxiliary;
using Origin.Display;

namespace Nsnbc
{
    public static class Library
    {      
        private static ContentManager content;
        private static Dictionary<ArtName, Texture2D> arts = new Dictionary<ArtName, Texture2D>();
        private static Dictionary<ArtName, Texture2D> flippedArts = new Dictionary<ArtName, Texture2D>();
        public static Texture2D Pixel { get; private set; }
        public static SpriteFont FontVerdana { get; private set; }
        public static Texture2D Circle1000X1000 { get; set; }
        public static Texture2D EmptyCircle1000X1000 { get; set; }
        public static Dictionary<string, Texture2D> Icons { get; set; }

        public static void Init(ContentManager incomingContent)
        {
           content = incomingContent;
           Pixel = content.Load<Texture2D>("Art\\Pixel");
           SpriteFont aSemi32 = content.Load<SpriteFont>("ASemi32");
           BitmapFontGroup.ASemi48 = new BitmapFontGroup(aSemi32, aSemi32, aSemi32, aSemi32, null);
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

        public static void LoadArt(ArtName art)
        {
            arts.Add(art, content.Load<Texture2D>("Art\\" + art));
        }
    }
}