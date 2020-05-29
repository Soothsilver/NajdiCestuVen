namespace Nsnbc.Android.Stories
{
    public class QEndFlyouts : QEvent
    {
        public override void Begin(Session session)
        {
            session.ActiveActities.RemoveAll(act => act is QFlyFromCenter);
        }
    }
}