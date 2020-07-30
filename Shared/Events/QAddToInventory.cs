using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Scenes.Prison
{
    [JsonObject(MemberSerialization.Fields)]
    public class QAddToInventory : QEvent
    {
        public ArtName Art { get; }

        public QAddToInventory(ArtName art)
        {
            Art = art;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.Session.Inventory.Add(new InventoryItem(Art));
        }
    }
}