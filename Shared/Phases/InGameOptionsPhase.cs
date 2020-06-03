using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Nsnbc.Android;
using Nsnbc.Auxiliary;

namespace Nsnbc
{
    public class InGameOptionsPhase : GamePhase
    {
        public bool FromMenu { get; }
        private Rectangle rectMenu = new Rectangle(Root.Screen.Width / 2 - 500, Root.Screen.Height / 2 - 400, 1000, 800);

        public InGameOptionsPhase(bool fromMenu = false)
        {
            FromMenu = fromMenu;
        }

        protected internal override void Initialize(Game game)
        {
            base.Initialize(game);
        }

        public override void Destruct(Game game)
        {
            base.Destruct(game);
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {   
            Primitives.FillRectangle(Root.Screen, Color.Black.Alpha(100));
            Primitives.DrawAndFillRectangle(rectMenu, Color.CornflowerBlue, Color.Blue, 2);

            int x = rectMenu.X + rectMenu.Width / 2 - 450;
            int y = rectMenu.Y + 50;
            int width = 900;
            int height = 120;
            Ux.DrawButton(new Rectangle(x, y, width, height),  FromMenu ? "Zpět" : "Ukončit hru do menu", () =>
            {
                Root.PopFromPhase();
                if (!FromMenu)
                {
                    Root.PhaseStack[Root.PhaseStack.Count - 2].Destruct(game);
                }
            });


            y += height + 20;
            Ux.DrawCheckbox(new Rectangle(x, y, width, height), "Automaticky přehrávat dialogy", () => LocalDataStore.AutoMode, () =>
            {
                LocalDataStore.AutoMode = !LocalDataStore.AutoMode;
            });

            if (!FromMenu)
            {
                Ux.DrawButton(new Rectangle(x, rectMenu.Height - 40, width, height), "Vrátit se ke hře", () => { Root.PopFromPhase(); });
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