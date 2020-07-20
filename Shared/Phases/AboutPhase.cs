using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nsnbc.Phases
{
    public class AboutPhase : GamePhase
    {
        protected internal override void Initialize(Game game)
        {
            Eqatec.Send("PHASE CREDITS");
            base.Initialize(game);
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            Rectangle r = new Rectangle(Root.Screen.Width / 2 - 600, Root.Screen.Height/2-500,1200,1000);
            Primitives.DrawAndFillRectangle(r, Color.LightYellow, Color.Black, 2);
            Writer.DrawString(
                G.Tn(@"Programování a kresba:
  Petr Hudeček (Profesor)

Hlasy:
  Tišík: Radim Křížovský (Rádio)
  Vědátor: Filip Petr (Krtek)
  Skok: Michael Roob (Perry)
  Akela: Josef Petr

Hra je odvozena od webového komiksu Naší snahou nejlepší buď čin, který najdete na adrese https://nsnbc.neocities.org nebo na Facebooku. 
"), r.Extend(-10,-10), Color.Black, BitmapFontGroup.Main40);
            Ux.DrawButton(new Rectangle(r.Right - 450, r.Bottom - 110, 400, 100), G.T("Zpět"),
                Root.PopFromPhase);
        }
    }
}