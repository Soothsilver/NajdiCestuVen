namespace Nsnbc.Events
{
    public class QPushZoomAndScene : QEvent
    {
        public override void Begin(Session session)
        {
            session.PushZoom();
        }
    }
}