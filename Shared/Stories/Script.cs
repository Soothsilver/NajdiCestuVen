using System.Collections.Generic;
using Nsnbc.Events;
using Nsnbc.Phases;

namespace Nsnbc.Stories
{
    public class Script
    {
        public List<QEvent> Events = new List<QEvent>();

        public Script(BookmarkId mainBookmark, QEvent[] events)
        {
            Events.Add(new QBookmark(mainBookmark));
            Events.AddRange(events);
        }
    }
}