using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nsnbc.Android;
using Nsnbc.Android.Stories;

namespace Nsnbc
{
    public class TSession
    {
        public bool SomebodyIsSpeaking;
        public string SpeakingText;
        public string SpeakingSpeaker;
        public Action<TSession> SpeakingAuxiAction { get; set; }
        public string SpeakingAuxiActionName;
        public ArtName SpeakerLeft { get; set; }
        public ArtName SpeakerRight { get; set; }

        public ArtName Background;
        
        public List<IQActivity> ActiveActities = new List<IQActivity>();
        public ImprovedQueue<QEvent> IncomingEvents = new ImprovedQueue<QEvent>();
        public Rectangle FullResolution = new Rectangle(0,0,1920,1080);
        public Rectangle CurrentZoom = new Rectangle(0,0,1920,1080);
        public SpeakerPosition SpeakerPosition { get; set; }
        public FirstScene Scene { get; set; }
        public bool YouHaveControl { get; set; }
        public Stack<Rectangle> ZoomStack { get; } = new Stack<Rectangle>();

        public void Enqueue(StoryID intro)
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
            ActiveActities.RemoveAll(act => act is QSpeak);
            Puzzle = puzzle;
            Puzzle.Begin(this);
        }

        public TrezorPuzzle Puzzle { get; set; }
        public List<InventoryItem> Inventory { get; set; } = new List<InventoryItem>();
        public bool FastForwarding { get; set; }
        public InventoryItem HeldItem { get; set; }

        public void ExitPuzzle()
        {
            Puzzle.Exit(this);
            Puzzle = null;
        }

        public void PopZoom()
        {
            this.CurrentZoom = this.ZoomStack.Pop();
            this.Scene.HideObjects = false;
        }

        public void PushZoom()
        {
            this.ZoomStack.Push(this.CurrentZoom);
            this.Scene.HideObjects = true;
        }
    }
}