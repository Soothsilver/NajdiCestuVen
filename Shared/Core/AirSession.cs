using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.PostSharp;
using Nsnbc.Stories;
using Nsnbc.Stories.Scenes;
using Nsnbc.Texts;
using Nsnbc.Util;

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
            this.Session = session;
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
        public void QuickEnqueue(params QEvent[] qEvents)
        {
            Session.IncomingEvents.QuickEnqueue(qEvents);
        }

        [Trace(AttributeExclude = true)]
        public bool FastForwarding { get; set; }
        
        public Scene? ActiveScene => Session.ActiveScene;

        public void QuickSpeak(string text)
        {
            this.QuickEnqueue(new QSpeak("", text, ArtName.Null, SpeakerPosition.Left));
        }
    }
}