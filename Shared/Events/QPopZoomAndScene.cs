namespace Nsnbc.Android.Stories
{
    public class QPopZoomAndScene : QEvent
    {
        public override void Begin(Session session)
        {
            session.PopZoom();
        }
    }
    

    public class QPushZoomAndScene : QEvent
    {
        public override void Begin(Session session)
        {
            session.PushZoom();
        }
    }
}