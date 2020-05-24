﻿using System;
using Microsoft.Xna.Framework.Input;
using Nsnbc.Auxiliary;

namespace Nsnbc.Android.Stories
{
    public class QSpeak : QEvent, IQActivity
    {
        private readonly string speaker;
        private readonly string text;
        private readonly ArtName sprite;
        private readonly SpeakerPosition position;

        public QSpeak(string speaker, string text, ArtName sprite, SpeakerPosition position)
        {
            this.speaker = speaker;
            this.text = text;
            this.sprite = sprite;
            this.position = position;
        }

        public override void Begin(TSession session)
        {
            if (position == SpeakerPosition.Left)
            {
                session.SpeakerLeft = sprite;
            }
            else
            {
                session.SpeakerRight = sprite;
            }
            session.SpeakingSpeaker = speaker;
            session.SpeakingText = text;
            session.SpeakerPosition = position;
            session.ActiveActities.Add(this);
            session.SpeakingAuxiAction = AuxiAction;
            session.SpeakingAuxiActionName = AuxiActionName;
        }

        public bool Blocking => true;
        public bool Dead { get; set; }
        public Action<TSession> AuxiAction { get; set; }
        public string AuxiActionName { get; set; }

        public void Run(TSession session, float elapsedSeconds)
        {
            if (Root.Keyboard_NewState.IsKeyDown(Keys.Tab) || session.FastForwarding)
            {
                this.Dead = true;
                session.QuickEnqueue(new QEndSpeaking());
                session.QuickEnqueue(new QWait(0.05f));
            }
            if (Root.WasMouseLeftClick || Root.WasTouchReleased)
            {
                session.SpeakingText = null;
                this.Dead = true;
                Root.WasMouseLeftClick = false;
                Root.WasTouchReleased = false;
            }
        }
    }

    public class QEndSpeaking : QEvent
    {
        public override void Begin(TSession session)
        {
            session.SpeakingText = null;
        }
    }
}