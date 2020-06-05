using System.Diagnostics;
using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nsnbc.Android;
using Nsnbc.Auxiliary;
using Nsnbc.Phases;
using Origin.Display;

namespace Nsnbc
{
    public class MainMenu
    {
        public void Draw()
        {
            Primitives.DrawImage(Library.Art(ArtName.Exterior), Root.Screen);
            Primitives.FillRectangle(Root.Screen, Color.White.Alpha(150));
            Root.SpriteBatch.Draw(Library.Art(ArtName.Logo), new Rectangle(1920/2-800/2,250,800,250), Color.White);
            Ux.DrawButton(new Rectangle(Root.Screen.Width / 2 - 300, Root.Screen.Height / 2 - 100, 600, 100), "Spustit hru",
                () =>
                {
                    MainLoop loop = new MainLoop();
                    loop.Session = new Session();
                    loop.Session.Enqueue(StoryId.Intro);
                    loop.ConsiderProceedingInQueue();
                    Root.PushPhase(new SessionPhase(loop));
                });
            Ux.DrawButton(new Rectangle(Root.Screen.Width / 2 - 300, Root.Screen.Height / 2 + 180, 600, 100), "O autorech",
                () =>
                {
                    Root.PushPhase(new AboutPhase());
                });
            Ux.DrawButton(new Rectangle(Root.Screen.Width / 2 - 300, Root.Screen.Height / 2 + 300, 600, 100), "Ukončit hru",
                () =>
                {
                    Process.GetCurrentProcess().Kill();
                });
            // Settings
            Rectangle rCog = new Rectangle(Root.Screen.Width - MainLoop.COGSIZE, 0, MainLoop.COGSIZE,MainLoop.COGSIZE);
            Primitives.DrawImage(Library.Art(ArtName.Cog64), rCog);
            if (Root.IsMouseOver(rCog))
            {
                Ux.ButtonHasPriority = true;
                Ux.MouseOverAction = () => Root.PushPhase(new InGameOptionsPhase(true));
            }
        }
    }

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
                () =>
                {
                    Root.PopFromPhase();
                });
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