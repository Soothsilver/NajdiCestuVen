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
    public class PrisonR2 : PrisonRoom
    {
        public int Trisek = 0;

        public override void Draw(AirSession airSession)
        {
            Primitives.DrawZoomed(Library.Art(ArtName.Guardroom2), Parent.CurrentZoom);
            if (FireExists)
            {
                Primitives.DrawZoomed(Library.Art(ArtName.R1Fire), Parent.CurrentZoom);
            }
            Ux.DrawImageButton(ArtName.TurnAround128Disabled, ArtName.TurnAround128, new Rectangle(Root.Screen.Width/2-64, Root.Screen.Height-128,128,128), () =>
            {
                Parent.ActiveRoom = Parent.Guardhouse1;
            });
        }
        public override ArtName YouAreHere => ArtName.R1_G2_MinimapIcon;
        public bool FireExists { get; set; }

        public void Initialize()
        {
            Items.Add(new Interactible(new Rectangle(355,596,212,142), BookmarkId.R1_G2_KamnaDole, G.T("TODO"))
            {
                OnItemUse = new KnownCode(KnownCodes.KamnaDole)
            });
            Items.Add(new Interactible(new Rectangle(689,225,247,137), BookmarkId.R1_G2_PravdaLaska, G.T("TODO")));
            Items.Add(new Interactible(new Rectangle(380,431,155,66), BookmarkId.R1_G2_KamnaNahore, G.T("TODO"))
            {
                OnItemUse = new KnownCode(KnownCodes.KamnaNahore)
            });
            Items.Add(new Interactible(new Rectangle(929,503,524,175), BookmarkId.R1_G2_Bludiste, G.T("TODO")));
            Items.Add(new Interactible(new Rectangle(1273,217,188,147), BookmarkId.R1_G2_Svitek, BookmarkId.R1_G2_Svitek2));
            Items.Add(new Interactible(new Rectangle(1596,509,172,250), new Script(BookmarkId.None, new QEvent[]
            {
                QSpeak.Quick("Yes!"),
                new QAddToInventory(ArtName.R1ZelenyKlic), 
            }), G.T("TODO")));
        }
    }
}