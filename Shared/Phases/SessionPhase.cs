using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nsnbc.Phases
{
    public class SessionPhase : GamePhase
    {
        private MainLoop mainLoop;

        public SessionPhase(MainLoop mainLoop)
        {
            this.mainLoop = mainLoop;
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            mainLoop.Draw(elapsedSeconds);
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            mainLoop.Update(elapsedSeconds);
        }
    }
}