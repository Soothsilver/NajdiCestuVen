using Auxiliary;
using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;

namespace Nsnbc.Android.Stories
{
    public class QFlyFromCenter : QEvent, IDrawableActivity
    {
        private readonly ArtName art;
        private readonly float seconds;
        private float percentage;
        private float percentageSpeed;

        public QFlyFromCenter(ArtName art, float seconds)
        {
            this.art = art;
            this.seconds = seconds;
            percentageSpeed = 1f / seconds;
        }

        public override void Begin(Session session)
        {
            session.ActiveActities.Add(this);
            session.QuickEnqueue(new QWait(seconds, true));
        }

        public bool Blocking => false;
        public bool Dead { get; set; }
        public void Run(Session session, float elapsedSeconds)
        {
            percentage += percentageSpeed * elapsedSeconds;
            if (percentage >= 1)
            {
                percentage = 1;
            }
        }

        public void Draw()
        {
            int width = (int) (Root.Screen.Width * percentage);
            int height = (int) (Root.Screen.Height * percentage);
            Primitives.DrawImage(Library.Art(art), new Rectangle(
                Root.Screen.Width/2-width/2,
                Root.Screen.Height/2-height/2,
                width,
                height
            ));
        }
    }
}