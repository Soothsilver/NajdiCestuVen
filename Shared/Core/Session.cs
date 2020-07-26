﻿using System;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.Sounds;
using Nsnbc.Stories;
using Nsnbc.Stories.Scenes;
using Nsnbc.Texts;

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

        public int FastForwardToIndex { get; set; } = -1;
        public Scene? ActiveScene => SceneStack.Peek();
        
        public void PopActiveScene()
        {
            SceneStack.Pop();
        }

        public void PushScene(Scene scene)
        {
            SceneStack.Push(scene);
            scene.Begin(this);
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