using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;

namespace Nsnbc.Android
{
    public class PhaseLoop
    {
        public static void EnterFirstPhase()
        {
            Root.PushPhase(new LoadingPhase());
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