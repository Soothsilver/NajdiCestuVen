using Nsnbc.Core;
using Nsnbc.Stories;

namespace Nsnbc.SerializableCode
{
    public class CodeInput
    {
        /// <summary>
        /// Gets or sets the inventory item that you were holding while triggering this code.
        /// </summary>
        public InventoryItem InventoryItem { get; set; }
        /// <summary>
        /// Gets or sets the current hard session.
        /// </summary>
        public Session HardSession { get; set; }
        /// <summary>
        /// Gets or sets the interactible that you interacted with to trigger this code.
        /// </summary>
        public Interactible Interactible { get; set; }
    }
}