﻿using System;
using Auxiliary;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Nsnbc.Android;
using Nsnbc.Sounds;

namespace Nsnbc.Events
{
    public class QSpeak : QEvent, IQActivity
    {
        private readonly GString speaker;
        private readonly GString text;
        private readonly ArtName sprite;
        private readonly SpeakerPosition position;
        private readonly Voice voice;
        private SoundEffectInstance? ongoingVoice;
        public bool Dead { get; private set; }
        public Action<Session>? AuxiAction { get; set; }
        public GString? AuxiActionName { get; set; }

        private float timeInHereSpent;

        public QSpeak(string speaker, GString text, ArtName sprite, SpeakerPosition position, Voice voice = Voice.Null)
        {
            this.speaker = G.T(speaker);
            this.text = text;
            this.sprite = sprite;
            this.position = position;
            this.voice = voice;
        }
        public QSpeak(string speaker, string text, ArtName sprite, SpeakerPosition position, Voice voice = Voice.Null)
        {
            this.speaker = G.T(speaker);
            this.text = G.T(text);
            this.sprite = sprite;
            this.position = position;
            this.voice = voice;
        }

        public override void Begin(Session session)
        {
            Sfxs.StopDotting();
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
            session.ActiveActivities.Add(this);
            session.SpeakingAuxiAction = AuxiAction;
            session.SpeakingAuxiActionName = AuxiActionName;
            if (voice != Voice.Null && Sfxs.Voices.ContainsKey(voice) && Settings.Instance.UseVoices)
            {
                ongoingVoice = Sfxs.PlayVoice(voice);
            }
            else if (!string.IsNullOrEmpty(speaker.ToString()))
            {
                Sfxs.BeginDotting(text.ToString());
            }
        }

        public bool Blocking => true;
        public void Update(Session session, float elapsedSeconds)
        {
            timeInHereSpent += elapsedSeconds;
            if (Root.KeyboardNewState.IsKeyDown(Keys.Tab) || session.FastForwarding || (ongoingVoice != null && ongoingVoice.State == SoundState.Stopped && Settings.Instance.AutoMode))
            {
                Dead = true;
                session.QuickEnqueue(new QEndSpeaking());
                session.QuickEnqueue(new QWait(0.05f));
            }
            else if (Root.KeyboardNewState.IsKeyDown(Keys.F1))
            {
                Dead = true;
                session.QuickEnqueue(new QEndSpeaking());
            }
            if (Root.WasMouseLeftClick || Root.WasTouchReleased)
            {
                if (timeInHereSpent >= 0.3f)
                {
                    session.SpeakingText = null;
                    Dead = true;
                    Root.WasMouseLeftClick = false;
                    Root.WasTouchReleased = false;
                }
            }
        }
    }
}