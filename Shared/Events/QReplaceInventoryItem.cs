using System.Globalization;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Sounds;
using Nsnbc.Stories;
using Nsnbc.Visiting;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)] 

    public class QReplaceInventoryItem : QEvent
    {
        public ArtName NextItem { get; }
        public string NextDescription { get; }

        public QReplaceInventoryItem(ArtName nextItem, string nextDescription)
        {
            this.NextItem = nextItem;
            this.NextDescription = nextDescription;
        }

        public override void Begin(AirSession airSession)
        {
            ArtName toReplace = airSession.Session.HeldItem!.Art;
            int index = airSession.Session.Inventory.FindIndex(item => item.Art == toReplace);
            if (index != -1)
            {
                airSession.Session.Inventory.RemoveAt(index);
                InventoryItem inventoryItem = new InventoryItem(NextItem, NextDescription);
                airSession.Session.Inventory.Insert(index, inventoryItem);
                airSession.AnimateItemGet(inventoryItem);
                Sfxs.Play(SoundEffectName.Harp);
                if (airSession.Session.HeldItem?.Art == toReplace)
                {
                    airSession.Session.HeldItem = null;
                }
            }
        }

        public override void Accept(Visitor visitor)
        {
            visitor.VisitQReplaceInventoryItem(this);
        }
    }
}