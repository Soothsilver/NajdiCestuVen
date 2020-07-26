using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Texts;

namespace Nsnbc.Phases
{
    public class InGameOptionsPhase : GamePhase
    {
        private readonly MainLoop mainLoop;
        private readonly Rectangle rectMenu = new Rectangle(Root.Screen.Width / 2 - 600, Root.Screen.Height / 2 - 500, 1200, 1000);

        public InGameOptionsPhase(MainLoop mainLoop)
        {
            this.mainLoop = mainLoop;
        }
        
        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {   
            Primitives.FillRectangle(Root.Screen, Color.Black.Alpha(100));
            Primitives.DrawAndFillRectangle(rectMenu, Color.CornflowerBlue, Color.Blue, 2);

            int x = rectMenu.X + rectMenu.Width / 2 - 450;
            int y = rectMenu.Y + 50;
            int width = 900;
            int height = 100;
            Ux.DrawButton(new Rectangle(x, y, width, height),  G.T("Ukončit hru do menu"), () =>
            {
                ConfirmationPhase.Confirm(G.T("Vrátit se do hlavního menu?"), () =>
                    {
                        Root.PopFromPhase();
                        Root.PopFromPhase();
                    }
                );
            });


            y += height + 20;
            Ux.DrawButton(new Rectangle(x, y, width, height), G.T("Uložit hru"), () =>
            {
                Root.PushPhase(new SaveGamePhase(mainLoop));
            });
            y += height + 20;
            Ux.DrawButton(new Rectangle(x, y, width, height), G.T("Načíst hru"), () =>
            {
                Root.PushPhase(new LoadGamePhase());
            });
            y += height + 20;
            Ux.DrawButton(new Rectangle(x, y, width, height), G.T("Nastavení"), ()=>Root.PushPhase(new SettingsPhase()));


           
            Ux.DrawButton(new Rectangle(x, rectMenu.Height - 40 - height, width, height), G.T("Vrátit se ke hře"), Root.PopFromPhase);
            
        }
    }
}