using System;

namespace Nsnbc.Android.Stories
{
    public class QAction : QEvent
    {
        private readonly Action<Session> func;

        public QAction(Action<Session> func)
        {
            this.func = func;
        }

        public override void Begin(Session session)
        {
            func(session);
        }
    }
}