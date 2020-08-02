using System.Collections.Generic;
using Newtonsoft.Json;
using Nsnbc.Core;
using PostSharp.Community.ToString;

namespace Nsnbc.Stories.Scenes
{
    [JsonObject(MemberSerialization.Fields)]
    public abstract class Room
    {
        [IgnoreDuringToString]
        public Scene Parent { get; set; }
        
        public abstract void Draw(AirSession airSession);
        public abstract ArtName YouAreHere { get; }
        public List<Interactible> Items { get; set; } = new List<Interactible>();
        public string Name { get; set; }
    }
}