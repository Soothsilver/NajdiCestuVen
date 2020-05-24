using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;

namespace Nsnbc
{
    public class InGameOptionsPhase : GamePhase
    {
        Rectangle rectMenu = new Rectangle(Root.Screen.Width / 2 - 500, Root.Screen.Height / 2 - 400, 1000, 800);

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {   
            Primitives.FillRectangle(Root.Screen, Color.Black.Alpha(100));
            Primitives.DrawAndFillRectangle(rectMenu, Color.CornflowerBlue, Color.Blue, 2);

            int x = rectMenu.X + rectMenu.Width / 2 - 450;
            int y = rectMenu.Y + 50;
            int width = 900;
            int height = 120;
            UX.DrawButton(new Rectangle(x, y, width, height), "Ukončit hru do menu", () =>
            {
                Root.PhaseStack.Pop();
                Root.PhaseStack.Pop();
            });
            
            UX.DrawButton(new Rectangle(x, rectMenu.Height - 40, width, height), "Vrátit se ke hře", () => {
                Root.PopFromPhase();
            });
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
                if (UX.MouseOverAction != null)
                {
                    UX.MouseOverAction();
                }
            }
        }
    }
}