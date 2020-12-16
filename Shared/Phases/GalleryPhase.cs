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

        readonly List<GalleryItem> pictures = new List<GalleryItem>();
        protected internal override void Initialize(Game game)
        {
            Eqatec.Send("PHASE GALLERY");
            pictures.Add(new CGGalleryItem( G.T("První promo (česky)"), ArtName.PromoArt1Czech));
            pictures.Add(new CGGalleryItem( G.T("První promo (anglicky)"), ArtName.PromoArt1English));
            pictures.Add(new CGGalleryItem( G.T("Smíšek prosí"), ArtName.SmisekProsi));
            pictures.Add(new CGGalleryItem(G.T("Postavy z Najdi cestu ven!"), ArtName.PostavyNajdiCestuVen));
            pictures.Add(new CGGalleryItem(G.T("Tišík a Vědátor se plaví"), ArtName.TisikVedatorNaKanoji_1920x1080_Tmavy));
            pictures.Add(new CGMultipleGalleryItem(G.T("Tišík na detektorul lži"), 
                ArtName.LieDetector1,
                ArtName.LieDetectorSpeaking,
                ArtName.LieDetectorShock));
            
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
    }
}