using Nsnbc.Core;
using Nsnbc.Sounds;
using Nsnbc.Stories;

namespace Nsnbc.Events
{
    public class QReplaceInventoryItem : QEvent
    {
        private readonly ArtName previousItem;
        private readonly ArtName nextItem;

        public static QReplaceInventoryItem ReplaceHeldItem(ArtName artName)
        {
            return new QReplaceInventoryItem(ArtName.Null, artName);
        }
        
        public QReplaceInventoryItem(ArtName previousItem, ArtName nextItem)
        {
            this.previousItem = previousItem;
            this.nextItem = nextItem;
        }

        public override void Begin(AirSession airSession)
        {
            ArtName toReplace = previousItem;
            if (toReplace == ArtName.Null)
            {
                toReplace = airSession.Session.HeldItem!.Art;
            }
            int index = airSession.Session.Inventory.FindIndex(item => item.Art == toReplace);
            if (index != -1)
            {
                airSession.Session.Inventory.RemoveAt(index);
                airSession.Session.Inventory.Insert(index, new InventoryItem(nextItem));
                Sfxs.Play(SoundEffectName.SfxHarp);
                if (airSession.Session.HeldItem?.Art == toReplace)
                {
                    airSession.Session.HeldItem = null;
                }
            }
        }
    }
}