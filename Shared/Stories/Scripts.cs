using System.Collections.Generic;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.Stories.Sets;

namespace Nsnbc.Stories
{
    public static class Scripts
    {
        public static Dictionary<BookmarkId, Script> All = new Dictionary<BookmarkId, Script>();

        public static void LoadAll()
        {
            foreach (Script script in TechDemo.CreateScripts())
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