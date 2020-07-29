using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.PostSharp;
using Nsnbc.Texts;
using PostSharp.Community.ToString;

namespace Nsnbc.Stories.Scenes.Prison
{
    [JsonObject(MemberSerialization.Fields), Trace]
    public class PrisonTableScene : Scene
    {
        [IgnoreDuringToString]
        private PrisonScene PrisonScene { get; set; }
        public override IEnumerable<Interactible> Interactibles => Items;
        public List<Interactible> Items { get; set; } = new List<Interactible>();
        public Interactible DrawerItem { get; set; }

        private VisibleInteractible Triska;
        public DrawerScene Drawer = new DrawerScene();

        public PrisonTableScene(PrisonScene prisonScene)
        {
            this.PrisonScene = prisonScene;
        }

        public override Scene? FindExistingScene(SceneName name)
        {
            return PrisonScene.FindExistingScene(name);
        }

        public override void Begin(Session hardSession)
        {
            base.Begin(hardSession);
        }

        public override void Draw(AirSession airSession)
        {
            Primitives.DrawZoomed(Library.Art(ArtName.R1_Table), this.CurrentZoom);
            Triska.Draw(this.CurrentZoom);
            Ux.DrawImageButton(ArtName.TurnAround128Disabled, ArtName.TurnAround128, new Rectangle(Root.Screen.Width/2-64, Root.Screen.Height-128,128,128), () =>
            {
                airSession.Enqueue(new QPopScene());
            });
        }

        public override void AfterPop(AirSession airSession)
        {           
            airSession.Enqueue(new QZoomInto(CommonGame.R1920x1080, 0.1f));
        }

        public void Initialize(Session hardSession)
        {
            Items.Add(new Interactible(new Rectangle(267,297,275,163), BookmarkId.R1_G1_Detektor, G.T("TODO") ));
            Items.Add(new Interactible(new Rectangle(548,377,150,85), BookmarkId.R1_G1_Navod, G.T("TODO") ));
            Script scrTriska = new Script();
            Triska = new VisibleInteractible(new Rectangle(20,729,71,83),  ArtName.R1_Table_Trisky, scrTriska, G.T("TODO") );
            Items.Add(Triska);
            scrTriska.Events.AddRange(new QEvent[]
            {
                new QSpeak("", "Beru to", ArtName.Null, SpeakerPosition.Left),
                new QMakeInvisible(Triska),
                new QAddToInventory(ArtName.R1Triska)
            });  
            Items.Add(new Interactible(new Rectangle(1020,530,423,105), BookmarkId.R1_G1_Suplik1, G.T("TODO") ));
            Items.Add(DrawerItem = new Interactible(new Rectangle(1020,657,423,97), new Script(BookmarkId.None, new QEvent[]
            {
                QSpeak.Quick("uuuuu"),
                new QPushScene(SceneName.R1_Suplik2), 
            }), SceneName.R1_Suplik2 ));
        }
    }
}