using System.Diagnostics;
using Auxiliary;
using Microsoft.Xna.Framework;
using Nsnbc.Events;
using Nsnbc.Phases;

namespace Nsnbc
{
    public class MainMenu
    {
        public void Draw()
        {
            Primitives.DrawImage(Library.Art(ArtName.Exterior), Root.Screen);
            Primitives.FillRectangle(Root.Screen, Color.White.Alpha(150));
            Root.SpriteBatch.Draw(Library.Art(ArtName.Logo), new Rectangle(1920/2-800/2,250,800,250), Color.White);
            Ux.DrawButton(new Rectangle(Root.Screen.Width / 2 - 300, Root.Screen.Height / 2 - 100, 600, 100), G.T("Spustit hru"),
                () =>
                {
                    MainLoop loop = new MainLoop();
                    loop.Session = new Session();
                    loop.Session.Enqueue(StoryId.Intro);
                    loop.ConsiderProceedingInQueue();
                    Root.PushPhase(new SessionPhase(loop));
                });
            Ux.DrawButton(new Rectangle(Root.Screen.Width / 2 - 300, Root.Screen.Height / 2 + 180, 600, 100), G.T("O autorech"),
                () =>
                {
                    Root.PushPhase(new AboutPhase());
                });
            Ux.DrawButton(new Rectangle(Root.Screen.Width - 450, Root.Screen.Height / 2 + 300, 400, 100), GetText.CurrentLanguage == Language.English ? "Čeština" : "English",
                () =>
                {
                    GetText.ToggleLanguage();
                });
            Ux.DrawButton(new Rectangle(Root.Screen.Width / 2 - 300, Root.Screen.Height / 2 + 300, 600, 100), G.T("Ukončit hru"),
                () =>
                {
                    Process.GetCurrentProcess().Kill();
                });
            // Settings
            Rectangle rCog = new Rectangle(Root.Screen.Width - MainLoop.Cogsize, 0, MainLoop.Cogsize,MainLoop.Cogsize);
            Primitives.DrawImage(Library.Art(ArtName.Cog64), rCog);
            if (Root.IsMouseOver(rCog))
            {
                Ux.ButtonHasPriority = true;
                Ux.MouseOverAction = () => Root.PushPhase(new InGameOptionsPhase(true));
            }
        }
    }
}