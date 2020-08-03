using System.Globalization;
using Nsnbc.Core;
using Nsnbc.Sounds;
using Nsnbc.Stories;

namespace Nsnbc.Events
{
    public class QReplaceInventoryItem : QEvent
    {
        private readonly ArtName nextItem;
        private readonly string nextDescription;

        public QReplaceInventoryItem(ArtName nextItem, string nextDescription)
        {
            this.nextItem = nextItem;
            this.nextDescription = nextDescription;
        }

        public override void Begin(AirSession airSession)
        {
            ArtName toReplace = airSession.Session.HeldItem!.Art;
            int index = airSession.Session.Inventory.FindIndex(item => item.Art == toReplace);
            if (index != -1)
            {
                airSession.Session.Inventory.RemoveAt(index);
                airSession.Session.Inventory.Insert(index, new InventoryItem(nextItem, nextDescription));
                Sfxs.Play(SoundEffectName.SfxHarp);
                if (airSession.Session.HeldItem?.Art == toReplace)
                {
                    airSession.Session.HeldItem = null;
                }
            }
        }
    }
}