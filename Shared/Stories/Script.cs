﻿using System.Collections.Generic;
using Nsnbc.Events;

namespace Nsnbc.Stories
{
    public class Script
    {
        public List<QEvent> Events { get; } = new List<QEvent>();

        public Script()
        {
            
        }
        public Script(params QEvent[] events)
        {
            Events.AddRange(events);
        }

        public Script(BookmarkId mainBookmark, QEvent[] events)
        {
            Events.Add(new QBookmark(mainBookmark));
            Events.AddRange(events);
        }

        public static implicit operator Script(QEvent qEvent)
        {
            return new Script
            {
                Events =
                {
                    qEvent
                }
            };
        }
    }
}