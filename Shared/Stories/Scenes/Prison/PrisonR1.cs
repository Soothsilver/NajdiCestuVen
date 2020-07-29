using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.PostSharp;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Scenes.Prison
{
    [JsonObject(MemberSerialization.Fields), Trace]
    public class PrisonR1 : PrisonRoom
    {
        public override void Draw(AirSession airSession)
        {
            Primitives.DrawZoomed(Library.Art(ArtName.Guardroom1), Parent.CurrentZoom);
            Primitives.DrawZoomed(Library.Art(ArtName.Guardroom1LockedGate), Parent.CurrentZoom);
            Ux.DrawImageButton(ArtName.TurnAround128Disabled, ArtName.TurnAround128, new Rectangle(Root.Screen.Width/2-64, Root.Screen.Height-128,128,128), () =>
            {
                Parent.ActiveRoom = Parent.Guardhouse2;
            });
            Ux.DrawImageButton(ArtName.Right128Disabled, ArtName.Right128, new Rectangle(Root.Screen.Width/2-64+128, Root.Screen.Height-128,128,128), () =>
            {
                Parent.ActiveRoom = Parent.Guardhouse3;
            });
        }
        public override ArtName YouAreHere => ArtName.R1_G1_MinimapIcon;

        public void Initialize()
        {
            Items.Add(new Interactible(new Rectangle(638,633,706,222), BookmarkId.R1_G1_Table, SceneName.R1_Table));
            Items.Add(new Interactible(new Rectangle(98, 245, 200, 600), BookmarkId.R1_G1_Gate, G.T("Futuristická brána ven. Není tu žádná klika ani klíčová dírka.")));
            Items.Add(new Interactible(new Rectangle(1580,663,83,54), BookmarkId.R1_G1_Electricity, G.T("Elektrická zásuvka. Mamka říká, ať na zásuvky nesahám.")));
            Items.Add(new Interactible(new Rectangle(1349, 399, 260, 256), BookmarkId.R1_G1_Fridge, SceneName.R1_Fridge));
            Items.Add(ZeleneDvere = new Interactible(new Rectangle(1772, 372, 109, 581), BookmarkId.R1_G1_GreenDoor, G.T("Dveře vedoucí do cely. Mají tmavě zelený zámek."))
            {
                OnItemUse = new KnownCode(KnownCodes.Dvere1)
            });
        }

        public Interactible ZeleneDvere { get; set; }
    }
}