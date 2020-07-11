namespace Nsnbc.Events
{
    public class QSetBackground : QEvent
    {
        private ArtName ArtName { get; }

        public QSetBackground(ArtName artName)
        {
            ArtName = artName;
        }

        public override void Begin(Session session)
        {
            session.ActiveActivities.RemoveAll(act => act is QZoomInto);
            session.Background = ArtName;
            session.CurrentZoom = session.FullResolution;
        }
    }
}