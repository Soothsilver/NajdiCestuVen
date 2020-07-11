using System;
using Nsnbc.PostSharp;

namespace Nsnbc.Events
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