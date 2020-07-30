using Newtonsoft.Json;

namespace Nsnbc.Stories.Scenes.Xml
{
    [JsonObject(MemberSerialization.Fields)]
    public class DirectionButton
    {
        public Script Script { get; set; }
    }
}