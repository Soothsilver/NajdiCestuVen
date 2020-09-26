using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)] 
    public class QPopScene : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            airSession.Session.PopActiveScene(airSession);
        }
    }
}