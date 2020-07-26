using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        
        // Visual novel
      
        // Activities
        public readonly List<IQActivity> ActiveActivities = new List<IQActivity>();
        
        // Display
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

        public void EnterPuzzle(TrezorPuzzle puzzle)
        {
            Session.IncomingEvents.Clear();
            ActiveActivities.RemoveAll(act => act is QSpeak.SpeakActivity);
            Puzzle = puzzle;
            Puzzle.Begin(this);
        }

        public TrezorPuzzle? Puzzle { get; set; }
        public List<InventoryItem> Inventory { get; } = new List<InventoryItem>();
        [Trace(AttributeExclude = true)]
        public bool FastForwarding { get; set; }
        public InventoryItem? HeldItem { get; set; }
        public Scene? ActiveScene => Session.ActiveScene;

        public void ExitPuzzle()
        {
            Puzzle!.Exit(this);
            Puzzle = null;
        }
    }
}