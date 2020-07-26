using Nsnbc.Core;

namespace Nsnbc.Events
{
    public class QEndFlyouts : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            airSession.ActiveActivities.RemoveAll(act => act is QFlyFromCenter.SelfActivity);
        }
    }
}