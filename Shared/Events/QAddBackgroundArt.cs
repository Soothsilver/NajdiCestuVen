using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    internal class QAddBackgroundArt : QEvent
    {
        private readonly ArtName art;

        public QAddBackgroundArt(ArtName art)
        {
            this.art = art;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.ActiveXmlScene!.Backgrounds.Add(art);
        }
    }
}