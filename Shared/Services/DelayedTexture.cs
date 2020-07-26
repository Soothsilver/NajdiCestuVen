using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;

namespace Nsnbc.Services
{
    public class DelayedTexture
    {
        public Texture2D Texture2D { get; private set; }

        private DelayedTexture(Texture2D t)
        {
            Texture2D = t;
        }
        public DelayedTexture(Stream filename)
        {
            this.Texture2D = Library.Art(ArtName.SlotLoading);
            Task.Run(() =>
            {
                var img = Texture2D.FromStream(Root.Graphics.GraphicsDevice, filename);
                this.Texture2D = img;
            });
        }

        public static DelayedTexture From(Texture2D art)
        {
            return new DelayedTexture(art);
        }
    }
}