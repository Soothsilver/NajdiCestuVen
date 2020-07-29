using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.Texts;

namespace Nsnbc.Stories
{
    public class InteractibleEncounter
    {
        public BookmarkId BookmarkId { get; private set; }
        public GString? SingleString { get; private set; }
        public Script? Script { get; private set; }
        public SceneName SceneName { get; private set; }

        public static implicit operator InteractibleEncounter(BookmarkId bookmarkId)
        {
            return new InteractibleEncounter() {BookmarkId = bookmarkId};
        }
        public static implicit operator InteractibleEncounter(Script script)
        {
            return new InteractibleEncounter() {Script = script};
        }
        public static implicit operator InteractibleEncounter(GString gString)
        {
            return new InteractibleEncounter() {SingleString = gString};
        }
        public static implicit operator InteractibleEncounter(SceneName sceneName)
        {
            return new InteractibleEncounter() {SceneName = sceneName};
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
    }
}