using System.Collections.Generic;
using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary.Fonts;

namespace Nsnbc.Phases
{
    public class TabbedPhase : GamePhase
    {
        public List<Tab> Tabs = new List<Tab>();
        public Tab SelectedTab = null!;
        public GString Caption;

        public TabbedPhase(GString caption)
        {
            Caption = caption;
        }
        
        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            
            Primitives.FillRectangle(Root.Screen, Color.Black.Alpha(100));
            Rectangle rectWindow = new Rectangle(10,10, Root.Screen.Width-20, Root.Screen.Height -20);
            Primitives.DrawAndFillRoundedRectangle(rectWindow, Theme.WindowBackground, Theme.WindowBorder, 2);
            
            // Title
            Rectangle rectWindowTitle = new Rectangle(rectWindow.X + 20, rectWindow.Y, rectWindow.Width, 80);
            Writer.DrawString(Caption, rectWindowTitle, Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.Left);
            
            // Tabs
            Rectangle rectTabs = new Rectangle(rectWindow.X + 5, rectWindowTitle.Bottom + 5, rectWindow.Width - 10, rectWindow.Bottom - rectWindowTitle.Bottom - 5 - 130);

            DrawTabs(rectTabs);
            
            // Button
            Rectangle rectBack = new Rectangle(rectTabs.Right - 400, rectTabs.Bottom + 20, 400, 100);
            Ux.DrawButton(rectBack, G.T("Zpět"), Root.PopFromPhase);
        }
        
        private void DrawTabs(Rectangle rectTabs)
        {
            int x = 0;
            int captionWidth = 500;
            Rectangle inside = new Rectangle(rectTabs.X + 15, rectTabs.Y + 100, rectTabs.Width - 30, rectTabs.Height - 110);
            foreach (Tab tab in Tabs)
            {
                bool selected = tab == SelectedTab;
                Rectangle rCaption = new Rectangle(rectTabs.X + x, rectTabs.Y, captionWidth, 80);
                bool mo = Root.IsMouseOver(rCaption);
                Primitives.FillRectangle(rCaption, mo ? Color.White : (selected ? Theme.WindowBackground : Color.LightBlue));
                Primitives.DrawLine(new Vector2(rCaption.X, rCaption.Y), new Vector2(rCaption.X, rCaption.Bottom), Color.Black, 2);
                Primitives.DrawLine(new Vector2(rCaption.X, rCaption.Y), new Vector2(rCaption.Right, rCaption.Y), Color.Black, 2);
                Primitives.DrawLine(new Vector2(rCaption.Right, rCaption.Y), new Vector2(rCaption.Right, rCaption.Bottom), Color.Black, 2);
                if (selected)
                {
                    tab.Draw(inside);
                }
                else
                {
                    Primitives.DrawLine(new Vector2(rCaption.X, rCaption.Bottom), new Vector2(rCaption.Right, rCaption.Bottom), Color.Black, 2);
                }
                Writer.DrawString(tab.Caption, rCaption.Extend(-3,-3), Color.Black, BitmapFontGroup.Main32, Writer.TextAlignment.Middle);
                if (mo)
                {
                    Ux.MouseOverAction = () => SelectedTab = tab;
                }
                x += captionWidth;
            }
            Primitives.DrawLine(new Vector2(rectTabs.X + x, rectTabs.Y + 80), new Vector2(rectTabs.Right, rectTabs.Y + 80), Color.Black, 2);
            Primitives.DrawLine(new Vector2(rectTabs.X, rectTabs.Y + 85), new Vector2(rectTabs.X, rectTabs.Bottom), Color.Black, 2);
            Primitives.DrawLine(new Vector2(rectTabs.Right, rectTabs.Y +85), new Vector2(rectTabs.Right, rectTabs.Bottom), Color.Black, 2);
            Primitives.DrawLine(new Vector2(rectTabs.X, rectTabs.Bottom), new Vector2(rectTabs.Right, rectTabs.Bottom), Color.Black, 2);
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            base.Update(game, elapsedSeconds);
        }
    }
}