using System.Collections.Generic;
using Auxiliary;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace Nsnbc.Phases
{
    public class GalleryPhase : TabbedPhase
    {
        public GalleryPhase() : base(G.T("Galerie"))
        {
        }

        List<GalleryItem> pictures = new List<GalleryItem>();
        protected internal override void Initialize(Game game)
        {
            Eqatec.Send("PHASE GALLERY");
            pictures.Add(new GalleryItem(ArtName.PromoArt1Czech, G.T("První promo (česky)")));
            pictures.Add(new GalleryItem(ArtName.PromoArt1English, G.T("První promo (anglicky)")));
            Tabs.Add(new Tab(G.T("Obrázky"), (r) =>
            {
                int startAt = 0;
                for (int y = 0; y < 2; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        if (startAt >= pictures.Count)
                        {
                            return;
                        }
                        DrawPicture(pictures[startAt], new Rectangle(r.X + x * 260, r.Y + y * 200, 250, 140)); 
                        startAt++;
                    }
                }
            }));
            Tabs.Add(new Tab(G.T("Postavy"), (r) =>
            {
                // TODO
                Writer.DrawString(G.T("Tato funkce ještě není implementována."), r, Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.Middle);
            }));
            SelectedTab = Tabs[0];
            base.Initialize(game);
        }

        private void DrawPicture(GalleryItem picture, Rectangle rect)
        {
            Primitives.DrawImage(Library.Art(picture.Art), rect);
            Writer.DrawString(picture.Caption, new Rectangle(rect.X, rect.Bottom, rect.Width, 150), Color.Black, BitmapFontGroup.Main24, Writer.TextAlignment.Top);
            bool mo = Root.IsMouseOver(rect);
            Primitives.DrawRectangle(rect, Color.Black, mo ? 3 : 1);
            if (mo)
            {      
                Ux.MouseOverAction = () => Root.PushPhase(new FullImagePhase(picture.Art));
            }
        }
    }

    internal class GalleryItem
    {
        public ArtName Art { get; }
        public string Caption { get; }

        public GalleryItem(ArtName art, string caption)
        {
            Art = art;
            Caption = caption;
        }
    }
}