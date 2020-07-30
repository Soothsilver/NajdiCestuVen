using Newtonsoft.Json;
using Nsnbc.Stories.Scenes.Xml;

namespace Nsnbc.Stories.Scenes
{
    [JsonObject(MemberSerialization.Fields)]
    public class Directions
    {
        public DirectionButton Turnaround { get; set; }
        public DirectionButton Left { get; set; }
        public DirectionButton Right { get; set; }
    }
}