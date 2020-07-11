using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nsnbc.PostSharp;

namespace Nsnbc.Phases
{
    public class AboutPhase : GamePhase
    {
        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            Rectangle r = new Rectangle(Root.Screen.Width / 2 - 600, Root.Screen.Height/2-500,1200,1000);
            Primitives.DrawAndFillRectangle(r, Color.LightYellow, Color.Black, 2);
            Writer.DrawString(
                @"Programování a kresba:
  Petr Hudeček (Profesor)

Hlasy:
  Tišík: Radim Křížovský (Rádio)
  Vědátor: Filip Petr (Krtek)
  Skok: Michael Roob (Perry)
  Akela: Josef Petr

Hra je odvozena od webového komiksu Naší snahou nejlepší buď čin, který najdete na adrese https://nsnbc.neocities.org nebo na Facebooku. 
", r.Extend(-10,-10), Color.Black, BitmapFontGroup.ASemi48);
            Ux.DrawButton(new Rectangle(r.Right - 450, r.Bottom - 110, 400, 100), "Zpět",
                Root.PopFromPhase);
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if (Root.WasKeyPressed(Keys.Escape))
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
            base.Update(game, elapsedSeconds);
        }
    }
}