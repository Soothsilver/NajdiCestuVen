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

        public QAddToInventory(ArtName art)
        {
            Art = art;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.Session.Inventory.Add(new InventoryItem(Art));
            Sfxs.Play(SoundEffectName.SfxHarp);
        }
    }
}