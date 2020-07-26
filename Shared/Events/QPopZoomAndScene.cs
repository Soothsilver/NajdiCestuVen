using Nsnbc.Core;

namespace Nsnbc.Events
{
    public class QPopZoomAndScene : QEvent
    {
        public override void Begin(Session session)
        {
            session.PopZoom();
        }
    }
}