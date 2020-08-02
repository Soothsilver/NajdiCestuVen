﻿using System.Collections.Generic;
using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.Stories;
using Nsnbc.Stories.Scenes;

namespace Nsnbc.Core
{
    [Trace]
    public class AirSession
    {
        public Session Session { get; }

     
        // Activities
        public readonly List<IQActivity> ActiveActivities = new List<IQActivity>();
        
        public AirSession(Session session)
        {
            Session = session;
        }
        
        public void Enqueue(Script script)
        {
            foreach (QEvent qEvent in script.Events)
            {
                Session.IncomingEvents.Enqueue(qEvent);
            }
        }
        public void QuickEnqueue(Script script)
        {
           Session.IncomingEvents.QuickEnqueue(script.Events.ToArray());
        }
        public void Enqueue(BookmarkId script)
        {
            Enqueue(Scripts.All[script]);
        }
        public void Enqueue(QEvent qEvent)
        {
            Session.IncomingEvents.Enqueue(qEvent);
        }
        public void QuickEnqueue(QEvent qEvent)
        {
            Session.IncomingEvents.QuickEnqueue(qEvent);
        }

        [Trace(AttributeExclude = true)]
        public bool FastForwarding { get; set; }
        
        public Scene? ActiveScene => Session.ActiveScene;
    }
}