using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Nsnbc.PostSharp;

namespace Nsnbc.Events
{
    public class QZoomInto : QEvent, IQActivity
    {
        private Rectangle afterZoom;
        private float seconds;
        private RectangleF speed;
        private RectangleF makingZoom;

        public QZoomInto(Rectangle afterZoom, float seconds)
        {
            this.afterZoom = afterZoom;
            this.seconds = seconds;
        }

        public override void Begin(Session session)
        {
            session.ActiveActivities.RemoveAll(ac => ac is QZoomInto);
            session.ActiveActivities.Add(this);
        }

        public bool Blocking => false;
        public bool Dead { get; private set; }

        public void Update(Session session, float elapsedSeconds)
        {
            if (speed == RectangleF.Empty)
            {
                afterZoom = Screenify(afterZoom, session.FullResolution);
                
                
                RectangleF distance = new RectangleF(afterZoom.X - session.CurrentZoom.X, afterZoom.Y - session.CurrentZoom.Y,
                    afterZoom.Width - session.CurrentZoom.Width, afterZoom.Height - session.CurrentZoom.Height);
                RectangleF lSpeed = new RectangleF(distance.X / seconds, distance.Y / seconds, distance.Width / seconds,
                    distance.Height / seconds);
                makingZoom = session.CurrentZoom;
                speed = lSpeed;
            }

            seconds -= elapsedSeconds;
            makingZoom = new RectangleF(makingZoom.X + speed.X * elapsedSeconds, makingZoom.Y + speed.Y * elapsedSeconds,
                makingZoom.Width + speed.Width * elapsedSeconds, makingZoom.Height + speed.Height * elapsedSeconds);
            session.CurrentZoom = (Rectangle) makingZoom;
            if (seconds <= 0)
            {
                Dead = true;
                session.CurrentZoom = afterZoom;
            }
        }
        [Trace(AttributeExclude = true)]

        private static Rectangle Screenify(Rectangle targetZoom, Rectangle fullScreen)
        {
            float targetAspectRatio = (float) targetZoom.Width / targetZoom.Height; 
            float correctAspectRatio = (float) fullScreen.Width / fullScreen.Height;
            
            // Increase to fit aspect ratio:
            if (targetAspectRatio > correctAspectRatio)
            {
                // Vertical needs to be increased
                int actualVerticalNeed = (int)(targetZoom.Width / correctAspectRatio);
                int moreVerticalNeeded = actualVerticalNeed - targetZoom.Height;
                targetZoom = new Rectangle(targetZoom.X, targetZoom.Y - moreVerticalNeeded / 2, targetZoom.Width, actualVerticalNeed);
            }
            else
            {
                // Horizontal needs to be increased
                int actualHorizontalNeed = (int) (targetZoom.Height * correctAspectRatio);
                int moreHorizontalNeed = actualHorizontalNeed - targetZoom.Width;
                targetZoom = new Rectangle(targetZoom.X - moreHorizontalNeed / 2, targetZoom.Y, actualHorizontalNeed, targetZoom.Height);
            }
            
            // Ensure it's not larger than fullscreen:
            if (targetZoom.X < 0)
            {
                targetZoom = new Rectangle(0, targetZoom.Y, targetZoom.Width, targetZoom.Height);
            }
            
            if (targetZoom.Y < 0)
            {
                targetZoom = new Rectangle(0, targetZoom.Y, targetZoom.Width, targetZoom.Height);
            }
            if (targetZoom.Right > fullScreen.Width)
            {
                int overflow = targetZoom.Right - fullScreen.Width;
                targetZoom = new Rectangle(targetZoom.X - overflow, targetZoom.Y, targetZoom.Width, targetZoom.Height);
            }
            
            if (targetZoom.Bottom > fullScreen.Height)
            {
                int overflow = targetZoom.Bottom - fullScreen.Height;
                targetZoom = new Rectangle(targetZoom.X, targetZoom.Y - overflow, targetZoom.Width, targetZoom.Height);
            }
            // overwidth-height check?
            return targetZoom;

        }
    }
}