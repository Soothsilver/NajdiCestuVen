using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QSetSpeakerArt : QEvent
    {
        private readonly ArtName speaker;
        private readonly SpeakerPosition left;

        public QSetSpeakerArt(ArtName speaker, SpeakerPosition left)
        {
            this.speaker = speaker;
            this.left = left;
        }

        public override void Begin(AirSession airSession)
        {
            if (left == SpeakerPosition.Left)
            {
                airSession.Session.CurrentLine.SpeakerLeft = speaker;
            }
            else
            {
                airSession.Session.CurrentLine.SpeakerRight = speaker;
            }
        }
    }
}