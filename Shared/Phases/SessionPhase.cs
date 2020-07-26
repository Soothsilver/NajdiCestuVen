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

        public SessionPhase(Session session)
        {
            MainLoop loop = new MainLoop();
            loop.Session = session;
            loop.ConsiderProceedingInQueue();
            mainLoop = loop;
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
            Sfxs.BeginSong(Truesong.Menu);
            base.Destruct(game);
        }
    }
}