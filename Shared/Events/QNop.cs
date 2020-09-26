using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    /// <summary>
    /// A qevent that does nothing.
    /// </summary>
    [JsonObject(MemberSerialization.Fields)] 
    internal class QNop : QEvent
    {
        public override void Begin(AirSession airSession)
        {
        }
    }
}