using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.Services;
using Nsnbc.Texts;

namespace Nsnbc.Phases
{
    public class ConfirmationPhase : GamePhase
    {
        public Action IfYes { get; }
        private readonly GString caption;

        public ConfirmationPhase(GString caption, Action ifYes)
        {
            IfYes = ifYes;
            this.caption = caption;
        }
        public static void Confirm(GString caption, Action ifYes)
        {
            if (Settings.Instance.ConfirmExitGame)
            {
                ConfirmationPhase cp = new ConfirmationPhase(caption, ifYes);
                Root.PushPhase(cp);
            }
            else
            {
                ifYes();
            }
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            Rectangle rect = new Rectangle(Root.Screen.Width/2-500, Root.Screen.Height/2-250,1000,500);
            Primitives.DrawAndFillRectangle(rect, Theme.WindowBackground, Color.DarkBlue, 2);
            Writer.DrawString(caption, new Rectangle(rect.X + 20, rect.Y + 20, rect.Width - 40, rect.Height - 120), Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.Middle);
            Ux.DrawButton(new Rectangle(rect.Center.X - 300, rect.Bottom - 110, 290, 100), G.T("Ano"), Yes, alignment: Writer.TextAlignment.Middle);
            Ux.DrawButton(new Rectangle(rect.Center.X + 10, rect.Bottom - 110, 290, 100), G.T("Ne"), No, alignment: Writer.TextAlignment.Middle);
        }

        private void Yes()
        {
            Root.PopFromPhase();
            IfYes();
        }

        private void No()
        {
            Root.PopFromPhase();
        }
    }
}