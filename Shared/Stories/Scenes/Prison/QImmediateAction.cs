using System;
using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Scenes.Prison
{
    public class QImmediateAction : QEvent
    {
        public Action Action { get; }

        public QImmediateAction(Action action)
        {
            Action = action;
        }

        public override void Begin(AirSession airSession)
        {
            Action();
        }

        public override bool PreventsSaving => true;
    }
}