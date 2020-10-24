﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.Phases.Galleries;
using Nsnbc.Texts;

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
            pictures.Add(new CGGalleryItem(ArtName.SmisekProsi, G.T("Smíšek prosí")));
            pictures.Add(new CGGalleryItem(ArtName.PostavyNajdiCestuVen, G.T("Postavy z Najdi cestu ven!")));
            pictures.Add(new CGGalleryItem(ArtName.TisikVedatorNaKanoji_1920x1080_Tmavy, G.T("Tišík a Vědátor se plaví")));
            
            Tabs.Add(new Tab(G.T("Obrázky"), r =>
            {
                Ux.DrawGallery(r, pictures);
            }));
            Tabs.Add(new Tab(G.T("Postavy"), r =>
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