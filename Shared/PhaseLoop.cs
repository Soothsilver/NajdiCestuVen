using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;

namespace Nsnbc.Android
{
    public class PhaseLoop
    {
        public static void EnterFirstPhase()
        {
            Root.PushPhase(new MainMenuPhase());
        }
        public static void Draw(GameTime gameTime)
        {
            Root.DrawPhase(gameTime);
        }
        
        public static void Update(GameTime gameTime)
        {
            Root.Update(gameTime);
        }
    }
}