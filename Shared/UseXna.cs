using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace NajdiCestuVenCommon
{
    public static class UseXna
    {
        public static void DX(SpriteBatch sb, Texture2D t)
        {
            sb.Draw(t, new Rectangle(10, 10, 100, 100), Color.White);
        }
    }
}
