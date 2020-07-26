using Nsnbc.Events;
using Nsnbc.Phases;

namespace Nsnbc.Core
{
    internal static class SessionLoader
    {
        public static Session LoadFromBookmark(BookmarkId bookmark)
        {
            Session session = new Session();
            session.Enqueue(StoryId.Intro);
            return session;
        }
    }
}