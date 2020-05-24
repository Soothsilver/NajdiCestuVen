using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nsnbc
{
    public static class UseXna
    {
        public static void Dx(SpriteBatch sb, Texture2D t)
        {
            sb.Draw(t, new Rectangle(10, 10, 100, 100), Color.White);
        }
    }
}
