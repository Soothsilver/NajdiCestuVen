using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nsnbc.Phases
{
    public class InGameOptionsPhase : GamePhase
    {
        private readonly bool fromMenu;
        private readonly Rectangle rectMenu = new Rectangle(Root.Screen.Width / 2 - 500, Root.Screen.Height / 2 - 400, 1000, 800);

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
            int height = 120;
            Ux.DrawButton(new Rectangle(x, y, width, height),  fromMenu ? G.T("Zpět") : G.T("Ukončit hru do menu"), () =>
            {
                Root.PopFromPhase();
                if (!fromMenu)
                {
                    Root.PhaseStack[^2].Destruct(game);
                }
            });


            y += height + 20;
            Ux.DrawCheckbox(new Rectangle(x, y, width, height), G.T("Automaticky přehrávat dialogy"), () => LocalDataStore.AutoMode, () =>
            {
                LocalDataStore.AutoMode = !LocalDataStore.AutoMode;
            }); 
            y += height + 20;
            Ux.DrawCheckbox(new Rectangle(x, y, width, height), G.T("Pípat při neozvučeném textu"), () => LocalDataStore.BeepingMode, () =>
            {
                LocalDataStore.BeepingMode = !LocalDataStore.BeepingMode;
            });

            if (!fromMenu)
            {
                Ux.DrawButton(new Rectangle(x, rectMenu.Height - 40, width, height), G.T("Vrátit se ke hře"), Root.PopFromPhase);
            }
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if (Root.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                Root.PopFromPhase();
            }

            if ((Root.WasMouseLeftClick || Root.WasTouchReleased))
            {
                Root.WasMouseLeftClick = false;
                Root.WasTouchReleased = false;
                if (Ux.MouseOverAction != null)
                {
                    Ux.MouseOverAction();
                }
            }
        }
    }
}