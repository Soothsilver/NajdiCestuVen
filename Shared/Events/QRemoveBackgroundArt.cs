using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    internal class QRemoveBackgroundArt : QEvent
    {
        private readonly ArtName art;

        public QRemoveBackgroundArt(ArtName art)
        {
            this.art = art;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.ActiveXmlScene!.Backgrounds.Remove(art);
        }
    }
}