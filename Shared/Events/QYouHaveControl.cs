using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Sets
{
    public class QYouHaveControl : QEvent
    {
        public bool YouHaveControl { get; }

        public QYouHaveControl(bool youHaveControl)
        {
            YouHaveControl = youHaveControl;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.Session.YouHaveControl = YouHaveControl;
        }
    }
}