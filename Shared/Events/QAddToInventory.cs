using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Sounds;
using Nsnbc.Stories;
using Nsnbc.Visiting;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QAddToInventory : QEvent
    {
        public ArtName Art { get; }
        public string ArtDescription { get; }

        public QAddToInventory(ArtName art, string artDescription)
        {
            Art = art;
            ArtDescription = artDescription;
        }

        public override void Begin(AirSession airSession)
        {
            InventoryItem inventoryItem = new InventoryItem(Art, ArtDescription);
            airSession.AddItemWithAnimation(inventoryItem);
            Sfxs.Play(SoundEffectName.Harp);
        }

        public override void Accept(Visitor visitor)
        {
            visitor.VisitQAddToInventory(this);
        }
    }
}