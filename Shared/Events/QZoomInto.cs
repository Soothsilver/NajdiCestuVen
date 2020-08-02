using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Util;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QZoomInto : QEvent
    {
        private readonly Rectangle afterZoom;
        private readonly float zoomDurationInSeconds;

        public QZoomInto(Rectangle afterZoom, float zoomDurationInSeconds)
        {
            this.afterZoom = afterZoom;
            this.zoomDurationInSeconds = zoomDurationInSeconds;
        }

        public override void Begin(AirSession airSession)
        {
            Require.NotNull(airSession.Session.ActiveScene);
            airSession.ActiveActivities.RemoveAll(ac => ac is ZoomActivity);
            Rectangle modifiedZoom = ModifyToAvoidOverwidthOverheight(afterZoom, CommonGame.R1920x1080);
            airSession.ActiveActivities.Add(new ZoomActivity(modifiedZoom, airSession.Session.ActiveScene.CurrentZoom, zoomDurationInSeconds));
        }

        public class ZoomActivity : IQActivity
        {
            private readonly Rectangle afterZoom;
            private readonly RectangleF speed;
            private RectangleF makingZoom;
            private float remainingSeconds;

            public ZoomActivity(Rectangle afterZoom, Rectangle currentZoom, float durationInSeconds)
            {
                this.afterZoom = afterZoom;
                remainingSeconds = durationInSeconds;
                RectangleF distance = new RectangleF(afterZoom.X - currentZoom.X, afterZoom.Y - currentZoom.Y,
                    afterZoom.Width - currentZoom.Width, afterZoom.Height - currentZoom.Height);
                makingZoom = currentZoom;
                speed = new RectangleF(distance.X / durationInSeconds, distance.Y / durationInSeconds, distance.Width / durationInSeconds, distance.Height / durationInSeconds);
            }

            public bool Blocking => false;
            public bool Dead { get; private set; }

            public void Update(AirSession airSession, float elapsedSeconds)
            {
                Require.NotNull(airSession.Session.ActiveScene);
                remainingSeconds -= elapsedSeconds;
                makingZoom = new RectangleF(
                    makingZoom.X + speed.X * elapsedSeconds,
                    makingZoom.Y + speed.Y * elapsedSeconds, 
                    makingZoom.Width + speed.Width * elapsedSeconds, 
                    makingZoom.Height + speed.Height * elapsedSeconds);
                airSession.Session.ActiveScene.CurrentZoom = (Rectangle)makingZoom;
                if (remainingSeconds <= 0)
                {
                    Dead = true;
                    airSession.Session.ActiveScene.CurrentZoom = afterZoom;
                }
            }
        }

        private static Rectangle ModifyToAvoidOverwidthOverheight(Rectangle targetZoom, Rectangle fullScreen)
        {
            float targetAspectRatio = (float) targetZoom.Width / targetZoom.Height;
            float correctAspectRatio = (float) fullScreen.Width / fullScreen.Height;

            // Increase to fit aspect ratio:
            if (targetAspectRatio > correctAspectRatio)
            {
                // Vertical needs to be increased
                int actualVerticalNeed = (int) (targetZoom.Width / correctAspectRatio);
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