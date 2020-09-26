using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)] 

    internal class QSetFlag : QEvent
    {
        public string FlagName { get; }

        public QSetFlag(string flagName)
        {
            FlagName = flagName;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.Session.Flags.Add(FlagName);
        }
    }
}