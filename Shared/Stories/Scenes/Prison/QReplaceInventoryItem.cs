using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Scenes.Prison
{
    public class QReplaceInventoryItem : QEvent
    {
        private readonly ArtName previousItem;
        private readonly ArtName nextItem;

        public QReplaceInventoryItem(ArtName previousItem, ArtName nextItem)
        {
            this.previousItem = previousItem;
            this.nextItem = nextItem;
        }

        public override void Begin(AirSession airSession)
        {
            int index = airSession.Session.Inventory.FindIndex(item => item.Art == previousItem);
            if (index != -1)
            {
                airSession.Session.Inventory.RemoveAt(index);
                airSession.Session.Inventory.Insert(index, new InventoryItem(nextItem));
            }
        }
    }
}