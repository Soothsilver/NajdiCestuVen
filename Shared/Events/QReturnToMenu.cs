using Nsnbc.Auxiliary;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    public class QReturnToMenu : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            airSession.Session.IncomingEvents.Clear();
            Root.PopFromPhase();
        }
    }
}