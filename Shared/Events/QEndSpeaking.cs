using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)] 
    public class QEndSpeaking : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            airSession.Session.CurrentLine.SpeakingText = null;
        }
    }
}