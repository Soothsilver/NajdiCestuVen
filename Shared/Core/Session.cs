using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.Sounds;
using Nsnbc.Stories;
using Nsnbc.Stories.Scenes;
using Nsnbc.Texts;
using Nsnbc.Util;

namespace Nsnbc.Core
{
    [JsonObject(MemberSerialization.Fields)]
    public class Session
    {
        public Script? CurrentScript { get; set; }
        public ImprovedQueue<QEvent> IncomingEvents { get; } = new ImprovedQueue<QEvent>();
        public ImprovedStack<Scene> SceneStack { get; } = new ImprovedStack<Scene>();
        public bool YouHaveControl { get; set; }
        public VisualNovelLine CurrentLine { get; } = new VisualNovelLine();
        public Songname CurrentMusic { get; set; }
        public List<InventoryItem> Inventory { get; } = new List<InventoryItem>();
        public InventoryItem? HeldItem { get; set; }
        
        public int FastForwardToIndex { get; set; } = -1;
        public Scene? ActiveScene => SceneStack.Peek();
        
        public void PopActiveScene(AirSession airSession)
        {
            SceneStack.Pop().AfterPop(airSession);
        }

        public void PushScene(Scene scene)
        {
            SceneStack.Push(scene);
            scene.Begin(this);
        }

        public void RemoveHeldItemFromInventory()
        {
            Require.NotNull(HeldItem);
            Inventory.Remove(HeldItem);
            HeldItem = null;
        }
    }

    public class VisualNovelLine
    {
        public GString? SpeakingText { get; set; }
        public GString? SpeakingSpeaker { get; set; }
        public QSpeak.AuxiliaryAction? SpeakingAuxiAction { get; set; }
        public ArtName SpeakerLeft { get; set; }
        public ArtName SpeakerRight { get; set; }
        public SpeakerPosition SpeakerPosition { get; set; }
    }
}