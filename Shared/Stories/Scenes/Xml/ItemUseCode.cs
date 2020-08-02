using System.Collections.Generic;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.SerializableCode;

namespace Nsnbc.Stories.Scenes.Xml
{
    [JsonObject(MemberSerialization.Fields)]
    internal class ItemUseCode : Code
    {
        private Dictionary<ArtName, Script> itemReactions = new Dictionary<ArtName, Script>();
        private Script theDefault;
        
        public override void Execute(CodeInput codeInput, AirSession airSession)
        {
            if (itemReactions.ContainsKey(codeInput.InventoryItem.Art))
            {
                airSession.Enqueue(itemReactions[codeInput.InventoryItem.Art]);
            }
            else
            {
                airSession.Enqueue(theDefault);
            }
        }

        public void AddResponse(ArtName item, Script script)
        {
            itemReactions.Add(item, script);
        }

        public void AddDefault(string defaultLine)
        {
            this.theDefault = QSpeak.Quick(defaultLine);
        } 
        public void AddDefault(Script defaultScript)
        {
            this.theDefault = defaultScript;
        }
    }
}