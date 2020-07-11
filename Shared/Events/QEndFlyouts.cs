namespace Nsnbc.Events
{
    public class QEndFlyouts : QEvent
    {
        public override void Begin(Session session)
        {
            session.ActiveActivities.RemoveAll(act => act is QFlyFromCenter);
        }
    }
}