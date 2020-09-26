using System;
using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)] 
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