using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nsnbc.Phases
{
    public class InGameOptionsPhase : GamePhase
    {
        private readonly bool fromMenu;
        private readonly Rectangle rectMenu = new Rectangle(Root.Screen.Width / 2 - 600, Root.Screen.Height / 2 - 500, 1200, 1000);

        public InGameOptionsPhase(bool fromMenu = false)
        {
            this.fromMenu = fromMenu;
        }
        
        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {   
            Primitives.FillRectangle(Root.Screen, Color.Black.Alpha(100));
            Primitives.DrawAndFillRectangle(rectMenu, Color.CornflowerBlue, Color.Blue, 2);

            int x = rectMenu.X + rectMenu.Width / 2 - 450;
            int y = rectMenu.Y + 50;
            int width = 900;
            int height = 100;
            Ux.DrawButton(new Rectangle(x, y, width, height),  fromMenu ? G.T("Zpět") : G.T("Ukončit hru do menu"), () =>
            {
                ConfirmationPhase.Confirm(G.T("Vrátit se do hlavního menu?"), () =>
                    {
                        Root.PopFromPhase();
                        Root.PopFromPhase();
                    }
                );
            });


            y += height + 20;
            Ux.DrawButton(new Rectangle(x, y, width, height), G.T("Uložit hru"), Root.PopFromPhase);
            y += height + 20;
            Ux.DrawButton(new Rectangle(x, y, width, height), G.T("Načíst hru"), Root.PopFromPhase);
            y += height + 20;
            Ux.DrawButton(new Rectangle(x, y, width, height), G.T("Nastavení"), ()=>Root.PushPhase(new SettingsPhase()));


           
            Ux.DrawButton(new Rectangle(x, rectMenu.Height - 40 - height, width, height), G.T("Vrátit se ke hře"), Root.PopFromPhase);
            
        }
    }
}