using Microsoft.Xna.Framework.Input;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes.Xml;

namespace Nsnbc.Stories.Scenes.Prison
{
    public class PrisonXmlScene : XmlScene
    {
        public override void Draw(AirSession airSession)
        {
            base.Draw(airSession);
            if (Root.WasKeyPressed(Keys.F8))
            {
                airSession.Session.Inventory.Add(new InventoryItem(ArtName.R1Sirky, "..."));
                airSession.Session.Inventory.Add(new InventoryItem(ArtName.R1Figurka, "..."));
                airSession.Session.Inventory.Add(new InventoryItem(ArtName.R1Hrnecek, "..."));
                airSession.Session.Inventory.Add(new InventoryItem(ArtName.R1Triska, "..."));
                airSession.Session.Inventory.Add(new InventoryItem(ArtName.R1Triska, "..."));
                airSession.Session.Inventory.Add(new InventoryItem(ArtName.R1Triska, "..."));
                airSession.Session.Inventory.Add(new InventoryItem(ArtName.R1PrazdnyPapir, "..."));
                airSession.Session.Inventory.Add(new InventoryItem(ArtName.R1Disketa, "..."));
                airSession.Session.Inventory.Add(new InventoryItem(ArtName.R1Baterie, "..."));
                airSession.Session.Inventory.Add(new InventoryItem(ArtName.R1ZelenyKlic, "..."));
            } 
        }
    }
}