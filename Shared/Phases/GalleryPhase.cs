using System.Collections.Generic;
using Auxiliary;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.Phases.Galleries;

namespace Nsnbc.Phases
{
    public class GalleryPhase : TabbedPhase
    {
        public GalleryPhase() : base(G.T("Galerie"))
        {
        }

        readonly List<GalleryItem> pictures = new List<GalleryItem>();
        protected internal override void Initialize(Game game)
        {
            Eqatec.Send("PHASE GALLERY");
            pictures.Add(new CGGalleryItem(ArtName.PromoArt1Czech, G.T("První promo (česky)")));
            pictures.Add(new CGGalleryItem(ArtName.PromoArt1English, G.T("První promo (anglicky)")));
            Tabs.Add(new Tab(G.T("Obrázky"), (r) =>
            {
                Ux.DrawGallery(r, pictures);
            }));
            Tabs.Add(new Tab(G.T("Postavy"), (r) =>
            {
                // TODO
                Writer.DrawString(G.T("Tato funkce ještě není implementována."), r, Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.Middle);
            }));
            SelectedTab = Tabs[0];
            base.Initialize(game);
        }

        private class CGGalleryItem : GalleryItem
        {
            public CGGalleryItem(ArtName art, GString caption) : base(art, caption, () => Root.PushPhase(new FullImagePhase(art)))
            {
            }
        }
    }
}