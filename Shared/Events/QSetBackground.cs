using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QSetBackground : QEvent
    {
        private ArtName ArtName { get; }

        public QSetBackground(ArtName artName)
        {
            ArtName = artName;
        }

        public override void Begin(AirSession airSession)
        {
            // Replace any currently active background.
            if (airSession.Session.ActiveScene is SimpleBackgroundScene)
            {
                airSession.Session.PopActiveScene(airSession);
            }

            airSession.ActiveActivities.RemoveAll(ac => ac is QZoomInto.ZoomActivity);
            // Set ourselves as the new background, at full zoom.
            airSession.Session.PushScene(new SimpleBackgroundScene(ArtName));
        }
    }
}