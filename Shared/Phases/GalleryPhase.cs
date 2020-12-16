using System.Collections.Generic;
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

        private readonly List<GalleryItem> pictures = new List<GalleryItem>();
        readonly List<GalleryItem> characters = new List<GalleryItem>();

        protected internal override void Initialize(Game game)
        {
            Eqatec.Send("PHASE GALLERY");
            pictures.Add(new CGGalleryItem(G.T("První promo (česky)"), ArtName.PromoArt1Czech));
            pictures.Add(new CGGalleryItem(G.T("První promo (anglicky)"), ArtName.PromoArt1English));
            pictures.Add(new CGGalleryItem(G.T("Smíšek prosí"), ArtName.SmisekProsi));
            pictures.Add(new CGGalleryItem(G.T("Postavy z Najdi cestu ven!"), ArtName.PostavyNajdiCestuVen));
            pictures.Add(new CGGalleryItem(G.T("Tišík a Vědátor se plaví"), ArtName.TisikVedatorNaKanoji_1920x1080_Tmavy));
            pictures.Add(new CGMultipleGalleryItem(G.T("Tišík na detektoru lži"),
                ArtName.LieDetector1,
                ArtName.LieDetectorSpeaking,
                ArtName.LieDetectorShock));

            characters.Add(new CharacterGalleryItem("Tišík"));
            characters.Add(new CharacterGalleryItem("Vědátor"));
            characters.Add(new CharacterGalleryItem("Vypravěč"));
            characters.Add(new CharacterGalleryItem("Smíšek"));
            characters.Add(new CharacterGalleryItem("Lenka"));
            characters.Add(new CharacterGalleryItem("Akela"));
            Tabs.Add(new Tab(G.T("Obrázky"), r => { Ux.DrawGallery(r, pictures); }));
            Tabs.Add(new Tab(G.T("Postavy"), r => { Ux.DrawGallery(r, characters); }));
            SelectedTab = Tabs[0];
            base.Initialize(game);
        }
    }
}