using System.Diagnostics;
using Auxiliary;
using Microsoft.Xna.Framework;
using Nsnbc.Android;
using Nsnbc.Auxiliary;
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
            Ux.DrawButton(new Rectangle(Root.Screen.Width / 2 - 300, Root.Screen.Height / 2, 600, 100), "Spustit hru",
                () =>
                {
                    MainLoop loop = new MainLoop();
                    loop.Session = new Session();
                    loop.Session.Enqueue(StoryId.Intro);
                    loop.ConsiderProceedingInQueue();
                    Root.PushPhase(new SessionPhase(loop));
                });
            Ux.DrawButton(new Rectangle(Root.Screen.Width / 2 - 300, Root.Screen.Height / 2 + 300, 600, 100), "Ukončit hru",
                () =>
                {
                    Process.GetCurrentProcess().Kill();
                });
        }
    }
}