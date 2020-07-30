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
        public VisibleInteractible(Rectangle rectangle, ArtName art, InteractibleEncounter firstEncounter, InteractibleEncounter secondEncounter) : base(rectangle, firstEncounter, secondEncounter)
        {
            VisualArt = art;
        }

        public void Draw(Rectangle zoom)
        {  
            if (VisualArt != ArtName.Null)
            {
                Primitives.DrawZoomed(Library.Art(VisualArt), zoom);
            }
        }
    }
}