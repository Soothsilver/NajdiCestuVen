using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.PostSharp;
using Nsnbc.Texts;

namespace Nsnbc.Stories
{
    [Trace, JsonObject(MemberSerialization.Fields)]
    public class VisibleInteractible : Interactible
    {
        public bool Visible { get; set; }
        public ArtName Art { get; set;  }

        public VisibleInteractible(Rectangle rectangle, ArtName art, InteractibleEncounter firstEncounter, InteractibleEncounter secondEncounter) : base(rectangle, firstEncounter, secondEncounter)
        {
            Art = art;
            Visible = true;
        }

        public void Draw(Rectangle zoom)
        {  
            if (Visible)
            {
                Primitives.DrawZoomed(Library.Art(Art), zoom);
            }
        }
    }
}