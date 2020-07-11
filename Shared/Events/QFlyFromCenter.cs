using Auxiliary;
using Microsoft.Xna.Framework;

namespace Nsnbc.Events
{
    public class QFlyFromCenter : QEvent, IDrawableActivity
    {
        private readonly ArtName art;
        private readonly float seconds;
        private float percentage;
        private readonly float percentageSpeed;
        public QFlyFromCenter(ArtName art, float seconds)
        {
            this.art = art;
            this.seconds = seconds;
            percentageSpeed = 1f / seconds;
        }

        public override void Begin(Session session)
        {
            session.ActiveActivities.Add(this);
            session.QuickEnqueue(new QWait(seconds, true));
        }

        // TODO when does this die?
        public bool Dead => false;
        public bool Blocking => false;
        public void Update(Session session, float elapsedSeconds)
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