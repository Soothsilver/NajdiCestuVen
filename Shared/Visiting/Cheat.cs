using System.Collections.Generic;
using System.Linq;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Stories;
using Nsnbc.Stories.Scenes.Xml;

namespace Nsnbc.Visiting
{
    public static class Cheat
    {
        public static void GetAllItems(Session session)
        {
            XmlScene topScene = (session.SceneStack.First() as XmlScene)!;
            ItemCollectingVisitor visitor = new ItemCollectingVisitor();
            topScene.Accept(visitor);
            foreach (KeyValuePair<ArtName,string> kvp in visitor.Items)
            {
                session.Inventory.Add(new InventoryItem(kvp.Key, kvp.Value + " (cheated)"));
            }
        }
    }

    public class ItemCollectingVisitor : Visitor
    {
        public Dictionary<ArtName, string> Items { get; } = new Dictionary<ArtName, string>();
        public override void VisitQReplaceInventoryItem(QReplaceInventoryItem qReplaceInventoryItem)
        {
            Items[qReplaceInventoryItem.NextItem] = qReplaceInventoryItem.NextDescription;
        }

        public override void VisitQAddToInventory(QAddToInventory qAddToInventory)
        {
            Items[qAddToInventory.Art] = qAddToInventory.ArtDescription;
        }
    }

    public abstract class Visitor
    {
        public virtual void VisitXmlScene(XmlScene xmlScene)
        {
        }

        public virtual void VisitInteractible(XmlInteractible xmlInteractible)
        {
            
        }

        public virtual  void VisitScript(Script script)
        {
            
        }

        public virtual  void VisitNonspecificQEvent(QEvent qEvent)
        {
            
        }

        public  virtual void VisitQAddToInventory(QAddToInventory qAddToInventory)
        {
            
        }

        public virtual  void VisitQReplaceInventoryItem(QReplaceInventoryItem qReplaceInventoryItem)
        {
            
        }
    }
}