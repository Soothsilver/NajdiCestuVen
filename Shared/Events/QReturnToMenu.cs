using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Sets
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