using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Stories;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QBookmark : QEvent
    {
        public BookmarkId Id { get; }

        public QBookmark(BookmarkId id)
        {
            Id = id;
        }
        public override void Begin(AirSession airSession)
        {
        }
    }
}