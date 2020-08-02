using System;
using System.Collections.Generic;
using Nsnbc.Events;
using Nsnbc.Stories.Scenes.Prison;
using Nsnbc.Stories.Sets;

namespace Nsnbc.Stories
{
    public static class Scripts
    {
        public static Dictionary<BookmarkId, Script> All = new Dictionary<BookmarkId, Script>();

        public static void LoadAll()
        {
            LoadScripts(TechDemo.CreateScripts());
            LoadScripts(PrisonDialog.GetScripts());
            foreach (var unusedBookmark in (BookmarkId[])Enum.GetValues(typeof(BookmarkId)))
            {
                if (!All.ContainsKey(unusedBookmark))
                {
                    All.Add(unusedBookmark, new Script(unusedBookmark, new QEvent[]
                    {
                        new QSpeak("", "Tento text nebyl sepsán.", ArtName.Null, SpeakerPosition.Left)
                    }));
                }
            }
        }

        private static void LoadScripts(IEnumerable<Script> scriptProducer)
        {
            foreach (Script script in scriptProducer)
            {
                foreach (QEvent scriptAction in script.Events)
                {
                    if (scriptAction is QBookmark bookmark)
                    {
                        All.Add(bookmark.Id, script);
                    }
                }
            }
        }
    }
}