using System;
using System.IO;
using System.Threading;
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

        public DelayedTexture(Func<Stream> streamProducer)
        {
            Texture2D = Library.Art(ArtName.SlotLoading);
            Task.Run(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        Stream stream = streamProducer();
                        var img = Texture2D.FromStream(Root.Graphics.GraphicsDevice, stream);
                        Texture2D = img;
                        return;
                    }
                    catch (Exception)
                    {
                        // We're probably still holding a handle to that file, let's wait for a bit.
                        await Task.Delay(i * 500);
                    }
                }
                Texture2D = Library.Art(ArtName.SlotQuestion);
            });
        }

        public static DelayedTexture From(Texture2D art)
        {
            return new DelayedTexture(art);
        }
    }
}