using Auxiliary;
using Microsoft.Xna.Framework;
using Nsnbc.PostSharp;

namespace Nsnbc
{
    public static class PhaseLoop
    {
        [Trace]
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