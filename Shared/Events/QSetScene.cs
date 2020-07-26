using Nsnbc.Core;
using Nsnbc.Stories;

namespace Nsnbc.Events
{
    public class QSetScene : QEvent
    {
        private readonly FirstScene? firstScene;

        public QSetScene(FirstScene? firstScene)
        {
            this.firstScene = firstScene;
        }

        public override void Begin(Session session)
        {
            session.Scene = firstScene;
            session.Scene?.Begin(session);
        }
    }
}