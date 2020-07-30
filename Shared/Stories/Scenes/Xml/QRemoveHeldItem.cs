using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Scenes.Xml
{
    internal class QRemoveHeldItem : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            airSession.Session.RemoveHeldItemFromInventory();
        }
    }
}