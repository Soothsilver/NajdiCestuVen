using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.PostSharp;
using Nsnbc.Sounds;

namespace Nsnbc.Phases
{    
    [Trace]
    public class SessionPhase : GamePhase
    {
        private readonly MainLoop mainLoop;

        public SessionPhase(AirSession airSession)
        {
            MainLoop loop = mainLoop = new MainLoop(airSession);
            Session hardSession = airSession.Session;
            if (hardSession.CurrentScript != null && hardSession.FastForwardToIndex != -1)
            {
                airSession.Enqueue(hardSession.CurrentScript);
                loop.FastForwardTo(hardSession.FastForwardToIndex);
                hardSession.FastForwardToIndex = -1;
                hardSession.CurrentScript = null;
            }
            loop.ConsiderProceedingInQueue();
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            mainLoop.Draw(elapsedSeconds);
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            mainLoop.Update(elapsedSeconds);
        }

        public override void Destruct(Game game)
        {
            Sfxs.BeginSong(Songname.Menu);
            base.Destruct(game);
        }
    }
}