using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.Stories;
using Nsnbc.Stories.Scenes;
using Nsnbc.Stories.Scenes.Xml;

namespace Nsnbc.Core
{
 //   [Trace]
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
        public void QuickEnqueue(BookmarkId script)
        {
            QuickEnqueue(Scripts.All[script]);
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

        [NotNull] public Scene ActiveScene => Session.ActiveScene;
        public XmlScene ActiveXmlScene => (XmlScene) Session.ActiveScene;
        public bool IsQueueEmpty => Session.IncomingEvents.Count == 0 && ActiveActivities.All(act => !act.Blocking);
    }
}