using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Sounds;
using Nsnbc.Stories;

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
            airSession.Session.Inventory.Add(new InventoryItem(Art, ArtDescription));
            Sfxs.Play(SoundEffectName.Harp);
        }
    }
}