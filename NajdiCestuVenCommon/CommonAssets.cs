using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NajdiCestuVenCommon
{
    public class CommonAssets : DrawableGameComponent
    {
        public Texture2D Swords;
        public static void Init(Game game)
        {
            
        }

        public CommonAssets(Game game) : base(game)
        {         
            Swords = new ContentManager(game.Services, "Content").Load<Texture2D>("Swords");
        }
    }
}
