using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QFlyFromCenter : QEvent
    {
        private readonly ArtName art;
        private readonly float seconds;
        private readonly float percentageSpeed;
        
        public QFlyFromCenter(ArtName art, float seconds)
        {
            this.art = art;
            this.seconds = seconds;
            percentageSpeed = 1f / seconds;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.ActiveActivities.Add(new SelfActivity(this));
            airSession.QuickEnqueue(new QWait(seconds, true));
        }

        public class SelfActivity : IDrawableActivity
        {
            // TODO when does this die?
            
            private readonly QFlyFromCenter parent;

            public SelfActivity(QFlyFromCenter parent)
            {
                this.parent = parent;
            }
            private float percentage;
            public bool Dead => false;
            public bool Blocking => false;
            
            public void Update(AirSession airSession, float elapsedSeconds)
            {
                percentage += parent.percentageSpeed * elapsedSeconds;
                if (percentage >= 1)
                {
                    percentage = 1;
                }
            }

            public void Draw()
            {
                int width = (int) (Root.Screen.Width * percentage);
                int height = (int) (Root.Screen.Height * percentage);
                Primitives.DrawImage(Library.Art(parent.art), new Rectangle(
                    Root.Screen.Width/2-width/2,
                    Root.Screen.Height/2-height/2,
                    width,
                    height
                ));
            }
        }

    }
}