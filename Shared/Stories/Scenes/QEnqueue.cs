using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Phases;

namespace Nsnbc.Stories.Scenes
{
    public class QEnqueue : QEvent
    {
        public BookmarkId TargetBookmark { get; }

        public QEnqueue(BookmarkId targetBookmark)
        {
            TargetBookmark = targetBookmark;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.Enqueue(Stories.Scripts.All[TargetBookmark]);
        }
    }
}