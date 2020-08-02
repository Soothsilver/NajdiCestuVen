using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.Sounds;
using Nsnbc.Stories;
using Nsnbc.Stories.Scenes;
using Nsnbc.Stories.Scenes.Xml;
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
        public List<string> Flags { get; set; } = new List<string>();
        public bool CanBeSaved => !IncomingEvents.Any(q => q.PreventsSaving);

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

        public Interactible? FindInteractible(string interactibleName)
        {
            foreach (Scene scene in SceneStack.Reverse<Scene>())
            {
                Interactible? interactible = scene.FindInteractibleInThisScene(interactibleName);
                if (interactible != null)
                {
                    return interactible;
                }
            }

            return null;
        }

        public Scene? FindExistingScene(string sceneName)
        {  
            foreach (Scene scene in SceneStack.Reverse<Scene>())
            {
                if (scene is XmlScene xmlScene && xmlScene.Name == sceneName)
                {
                    return scene;
                }
                Scene? interactible = scene.FindExistingScene(sceneName);
                if (interactible != null)
                {
                    return interactible;
                }
            }
            return null;
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