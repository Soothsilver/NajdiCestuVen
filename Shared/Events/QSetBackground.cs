using System.IO.Pipes;
using Microsoft.Xna.Framework;

namespace Nsnbc.Android.Stories
{
    public class QSetBackground : QEvent
    {
        public ArtName ArtName { get; }

        public QSetBackground(ArtName artName)
        {
            ArtName = artName;
        }

        public override void Begin(TSession session)
        {
            session.ActiveActities.RemoveAll(act => act is QZoomInto);
            session.Background = ArtName;
            session.CurrentZoom = session.FullResolution;
        }
    }
}