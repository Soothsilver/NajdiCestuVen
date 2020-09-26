using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    /// <summary>
    /// Removes the item the player current holds from the inventory, and resets held item to null.
    /// </summary>
    [JsonObject(MemberSerialization.Fields)] 
    internal class QRemoveHeldItem : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            airSession.Session.RemoveHeldItemFromInventory();
        }
    }
}