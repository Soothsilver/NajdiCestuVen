using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.Sounds;
using Nsnbc.Stories;

namespace Nsnbc.Core
{
    [Trace]
    internal static class SessionLoader
    {
        public static AirSession LoadFromBookmark(BookmarkId bookmark)
        {
            Session session = new Session();
            Script currentScript = Scripts.All[bookmark];
            session.CurrentScript = currentScript;
            session.FastForwardToIndex = currentScript.Events.FindIndex(qb => qb is QBookmark qbe && qbe.Id == bookmark);
            AirSession airSession = new AirSession(session);
            return airSession;
        }
        public static AirSession LoadFromHardSession(Session hardSession)
        {
            Session session = hardSession;
            AirSession airSession = new AirSession(session);
            if (session.CurrentLine.SpeakingText != null)
            {
                airSession.ActiveActivities.Add(new ClickToContinueActivity());
            }
            Sfxs.BeginSong(Truesong.ByName(session.CurrentMusic));
            return airSession;
        }
    }
}