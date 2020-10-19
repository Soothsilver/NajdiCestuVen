using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Texts;

namespace Nsnbc.Stories
{
    public class InteractibleEncounter
    {
        public BookmarkId BookmarkId { get; set; }
        public GString? SingleString { get; set; }
        public Script? Script { get; set; }
        public SceneName SceneName { get; set; }

        public static implicit operator InteractibleEncounter(BookmarkId bookmarkId)
        {
            return new InteractibleEncounter {BookmarkId = bookmarkId};
        }
        public static implicit operator InteractibleEncounter(Script script)
        {
            return new InteractibleEncounter {Script = script};
        }
        public static implicit operator InteractibleEncounter(GString gString)
        {
            return new InteractibleEncounter {SingleString = gString};
        }
        public static implicit operator InteractibleEncounter(SceneName sceneName)
        {
            return new InteractibleEncounter {SceneName = sceneName};
        }

        public void Enqueue(AirSession airSession)
        {
            if (SingleString != null)
            {
                airSession.Enqueue(new QSpeak("", SingleString, ArtName.Null, SpeakerPosition.Left));
            }
            else if (SceneName != SceneName.None)
            {
                airSession.Enqueue(new QPushScene(SceneName));
            }
            else if (Script != null)
            {
                airSession.Enqueue(Script);
            }
            else
            {
                airSession.Enqueue(BookmarkId);
            }
        }
        public void QuickEnqueue(AirSession airSession)
        {
            if (SingleString != null)
            {
                airSession.QuickEnqueue(new QSpeak("", SingleString, ArtName.Null, SpeakerPosition.Left));
            }
            else if (SceneName != SceneName.None)
            {
                airSession.QuickEnqueue(new QPushScene(SceneName));
            }
            else if (Script != null)
            {
                airSession.QuickEnqueue(Script);
            }
            else
            {
                airSession.QuickEnqueue(BookmarkId);
            }
        }
    }
}