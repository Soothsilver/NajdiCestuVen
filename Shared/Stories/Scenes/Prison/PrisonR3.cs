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
    public class PrisonR3 : PrisonRoom
    {
        private VisibleInteractible Triska;
        private VisibleInteractible Hrnicek;

        public override void Draw(AirSession airSession)
        {
            Primitives.DrawZoomed(Library.Art(ArtName.Guardroom3), Parent.CurrentZoom);

            Triska.Draw(Parent.CurrentZoom);
            Hrnicek.Draw(Parent.CurrentZoom);

            Ux.DrawImageButton(ArtName.Left128Disabled, ArtName.Left128, new Rectangle(Root.Screen.Width/2-64-128, Root.Screen.Height-128,128,128), () =>
            {
                Parent.ActiveRoom = Parent.Guardhouse1;
            });
        }
        public override ArtName YouAreHere => ArtName.R1_G3_MinimapIcon;

        public void Initialize()
        {
            Script dvere1 = new Script();
            dvere1.Events.Add(new QSpeak("A","B", ArtName.Null, SpeakerPosition.Left));
            dvere1.Events.Add(new QGoToRoom(Parent.Guardhouse1));
            Script dvere2 = new Script();
            dvere2.Events.Add(new QGoToRoom(Parent.Guardhouse1));
            
            Items.Add(new Interactible(new Rectangle(38, 373, 107, 580), dvere1, dvere2));
            Items.Add(new Interactible(new Rectangle(506,349,237,197), BookmarkId.R1_G3_Umyvadlo, G.T("TODO"))
            {
                OnItemUse = new KnownCode(KnownCodes.Umyvadlo)
            });
            Items.Add(new Interactible(new Rectangle(1623,243,231,316), BookmarkId.R1_G3_Okenko, G.T("TODO")));
            Items.Add(new Interactible(new Rectangle(1447,247,155,423), BookmarkId.R1_G3_CelaDvere, G.T("TODO")));
            Items.Add(new Interactible(new Rectangle(1011,357,199,61), BookmarkId.R1_G3_Postel, G.T("TODO")));
            Script scriptTriska = new Script();
            Script scriptHrnecek = new Script();
            Items.Add(this.Triska = new VisibleInteractible(new Rectangle(1068,414,59,41), ArtName.R1_G3_Triska, scriptTriska, G.T("TODO")));
            Items.Add(this.Hrnicek = new VisibleInteractible(new Rectangle(1285,401,52,49), ArtName.R1_G3_Hrnecek, scriptHrnecek, G.T("TODO")));
            scriptTriska.Events.AddRange(new QEvent[]
            {
                new QSpeak("", "Beru to", ArtName.Null, SpeakerPosition.Left),
                new QMakeInvisible(Triska),
                new QAddToInventory(ArtName.R1Triska)
            });
            scriptHrnecek.Events.AddRange(new QEvent[]
            {
                new QSpeak("", "Beru to", ArtName.Null, SpeakerPosition.Left),
                new QMakeInvisible(Hrnicek),
                new QAddToInventory(ArtName.R1Hrnecek)
            });
        }
    }
}