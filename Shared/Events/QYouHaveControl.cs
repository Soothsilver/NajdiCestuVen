using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{  
    [JsonObject(MemberSerialization.Fields)] 

    public class QYouHaveControl : QEvent
    {
        public bool YouHaveControl { get; }

        public QYouHaveControl(bool youHaveControl)
        {
            YouHaveControl = youHaveControl;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.Session.YouHaveControl = YouHaveControl;
        }
    }
}