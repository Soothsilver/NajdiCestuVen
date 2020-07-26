using Nsnbc.Core;
using Nsnbc.Phases;

namespace Nsnbc.Events
{
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