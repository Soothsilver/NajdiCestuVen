using Nsnbc.Visiting;

namespace Nsnbc.Stories.Scenes.Xml
{
    public class XmlInteractible : Interactible
    {
        public string Name { get; set; }

        public void Accept(Visitor visitor)
        {
            visitor.VisitInteractible(this);
            FirstEncounter.Script?.Accept(visitor);
            SecondEncounter?.Script?.Accept(visitor);
            OnItemUse?.Accept(visitor);
        }
    }
}