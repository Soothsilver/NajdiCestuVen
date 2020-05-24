using System;

namespace Nsnbc.Android.Stories
{
    public class QAction : QEvent
    {
        private readonly Action<TSession> func;

        public QAction(Action<TSession> func)
        {
            this.func = func;
        }

        public override void Begin(TSession session)
        {
            func(session);
        }
    }
}