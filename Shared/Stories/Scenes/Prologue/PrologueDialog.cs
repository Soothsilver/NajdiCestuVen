using System.Collections.Generic;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Sounds;

namespace Nsnbc.Stories.Scenes.Prologue
{
    public class PrologueDialog
    {
        public static IEnumerable<Script> GetScripts()
        {
            yield return new Script(BookmarkId.Prologue,
                new QEvent[]
                {
                    new QBeginSong(Songname.Story), 
                    new QPushScene(SceneName.PrologueXML)
                });
        }
    }

    [JsonObject(MemberSerialization.Fields)]
    public class QGoToBookmark : QEvent
    {
        private readonly BookmarkId bookmarkId;

        public QGoToBookmark(BookmarkId bookmarkId)
        {
            this.bookmarkId = bookmarkId;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.QuickEnqueue(bookmarkId);
        }
    }
}