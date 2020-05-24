﻿namespace Nsnbc.Android.Stories
{
    public class QSetScene : QEvent
    {
        private readonly FirstScene firstScene;

        public QSetScene(FirstScene firstScene)
        {
            this.firstScene = firstScene;
        }

        public override void Begin(TSession session)
        {
            session.Scene = firstScene;
            session.Scene?.Begin(session);
        }
    }
}