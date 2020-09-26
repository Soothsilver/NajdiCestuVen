using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Phases.Galleries;
using Nsnbc.Stories;
using Nsnbc.Texts;

namespace Nsnbc.Phases
{
    internal class LoadScenePhase : TabbedPhase
    {
        private readonly List<GalleryItem> scenes = new List<GalleryItem>();
        protected internal override void Initialize(Game game)
        {
            base.Initialize(game);
            Eqatec.Send("PHASE LOAD SCENE");
            
            scenes.Add(new SceneGalleryItem(ArtName.Exterior, G.T("Technické demo"), BookmarkId.TechDemoStart));
            scenes.Add(new SceneGalleryItem(ArtName.InteriorGood, G.T("Technické demo: Zamčeni v Chatě teroru"), BookmarkId.TechDemo_Level));
            scenes.Add(new SceneGalleryItem(ArtName.PromoArt1Czech, G.T("Prolog"), BookmarkId.Prologue));
            scenes.Add(new SceneGalleryItem(ArtName.Guardroom1, G.T("Místnost 1: Vězení"), BookmarkId.R1_Guardhouse_Xml_Level));
            scenes.Add(new SceneGalleryItem(ArtName.CourtyardDemo, G.T("Místnost 2: Nádvoří"), BookmarkId.R2_Courtyard));
            
            Tabs.Add(new Tab(G.T("Scény"), r =>
            {
                Ux.DrawGallery(r, scenes);
            }));
            SelectedTab = Tabs[0];
        }

        private class SceneGalleryItem : GalleryItem
        {
            public SceneGalleryItem(ArtName screenshot, GString caption, BookmarkId bookmark) : base(screenshot, caption, () =>
            {
                Eqatec.Send("LOADSCENE " + bookmark);
                Root.PopFromPhase();
                Root.PushPhase(new SessionPhase(SessionLoader.LoadFromBookmark(bookmark)));
            })
            {
            }
        }

        public LoadScenePhase() : base(G.T("Pokračovat od scény"))
        {
        }
    }
}