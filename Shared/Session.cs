using System;
using System.Collections.Generic;
using Auxiliary;
using Microsoft.Xna.Framework;
using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.Stories;

namespace Nsnbc
{
    [Trace]
    public class Session
    {
        public string? SpeakingText;
        public string? SpeakingSpeaker;
        public Action<Session>? SpeakingAuxiAction { get; set; }
        public string? SpeakingAuxiActionName;
        public ArtName SpeakerLeft { get; set; }
        public ArtName SpeakerRight { get; set; }

        public ArtName Background;
        
        public readonly List<IQActivity> ActiveActivities = new List<IQActivity>();
        public readonly ImprovedQueue<QEvent> IncomingEvents = new ImprovedQueue<QEvent>();
        public Rectangle FullResolution = new Rectangle(0,0,1920,1080);
        public Rectangle CurrentZoom = new Rectangle(0,0,1920,1080);
        public SpeakerPosition SpeakerPosition { get; set; }
        public FirstScene? Scene { get; set; }
        public bool YouHaveControl { get; set; }
        private Stack<Rectangle> ZoomStack { get; } = new Stack<Rectangle>();

        public void Enqueue(StoryId intro)
        {
            foreach (QEvent qEvent in FullStory.EnqueueStory(intro))
            {
                IncomingEvents.Enqueue(qEvent);
            }
        }
        public void Enqueue(QEvent @event)
        {
            IncomingEvents.Enqueue(@event);
        }

        public void QuickEnqueue(QEvent @event)
        {
            IncomingEvents.QuickEnqueue(@event);
        }

        public void EnterPuzzle(TrezorPuzzle puzzle)
        {
            IncomingEvents.Clear();
            ActiveActivities.RemoveAll(act => act is QSpeak);
            Puzzle = puzzle;
            Puzzle.Begin(this);
        }

        public TrezorPuzzle? Puzzle { get; set; }
        public List<InventoryItem> Inventory { get; } = new List<InventoryItem>();
        [Trace(AttributeExclude = true)]
        public bool FastForwarding { get; set; }
        public InventoryItem? HeldItem { get; set; }

        public void ExitPuzzle()
        {
            Puzzle!.Exit(this);
            Puzzle = null;
        }

        public void PopZoom()
        {
            CurrentZoom = ZoomStack.Pop();
            Scene!.HideObjects = false;
        }

        public void PushZoom()
        {
            ZoomStack.Push(CurrentZoom);
            Scene!.HideObjects = true;
        }
    }
}