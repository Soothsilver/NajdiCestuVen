using Nsnbc.Core;

namespace Nsnbc.Events
{
    public class QPopScene : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            airSession.Session.PopActiveScene(airSession);
        }
    }
}