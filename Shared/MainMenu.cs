using System.Diagnostics;
using Auxiliary;
using Microsoft.Xna.Framework;
using Nsnbc.Android;
using Nsnbc.Android.Stories;
using Nsnbc.Auxiliary;
using Nsnbc.Phases;

namespace Nsnbc
{
    public class MainMenu
    {
        public void Draw()
        {
            Primitives.DrawImage(Library.Art(ArtName.PotemnelaChodba3), Root.Screen);
            Primitives.FillRectangle(Root.Screen, Color.White.Alpha(150));
            Root.SpriteBatch.Draw(Library.Art(ArtName.Logo), new Rectangle(1920/2-800/2,250,800,250), Color.White);
            UX.DrawButton(new Rectangle(Root.Screen.Width / 2 - 300, Root.Screen.Height / 2, 600, 100), "Spustit hru",
                () =>
                {
                    MainLoop loop = new MainLoop();
                    loop.Session = new TSession();
                    loop.Session.Enqueue(StoryID.Intro);
                    loop.ConsiderProceedingInQueue();
                    Root.PushPhase(new SessionPhase(loop));
                });
            UX.DrawButton(new Rectangle(Root.Screen.Width / 2 - 300, Root.Screen.Height / 2 + 300, 600, 100), "Ukončit hru",
                () =>
                {
                    Process.GetCurrentProcess().Kill();
                });
        }
    }
}