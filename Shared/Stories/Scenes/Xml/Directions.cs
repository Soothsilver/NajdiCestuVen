using Newtonsoft.Json;

namespace Nsnbc.Stories.Scenes.Xml
{
    [JsonObject(MemberSerialization.Fields)]
    public class Directions
    {
        public DirectionButton Turnaround { get; set; }
        public DirectionButton Left { get; set; }
        public DirectionButton Right { get; set; }
    }
}