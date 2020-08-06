using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Sounds;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QBeginSong : QEvent
    {
        [JsonProperty]
        private readonly Songname songname;

        public QBeginSong(Songname songname)
        {
            this.songname = songname;
        }
        
        public override void Begin(AirSession airSession)
        {
            airSession.Session.CurrentMusic = songname;
            Sfxs.BeginSong(songname);
        }
    }
}