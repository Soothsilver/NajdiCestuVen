using System;
using Microsoft.Xna.Framework.Audio;
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
        private readonly Voice voice;
        private SoundEffectInstance ongoingVoice;

        public QSpeak(string speaker, string text, ArtName sprite, SpeakerPosition position, Voice voice = Voice.Null)
        {
            this.speaker = speaker;
            this.text = text;
            this.sprite = sprite;
            this.position = position;
            this.voice = voice;
        }

        public override void Begin(Session session)
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
            if (voice != Voice.Null && Sfxs.Voices.ContainsKey(voice))
            {
                ongoingVoice = Sfxs.PlayVoice(Sfxs.Voices[voice]);
            }
            else
            {
                Sfxs.BeginDotting();
            }
        }

        public bool Blocking => true;
        public bool Dead { get; set; }
        public Action<Session> AuxiAction { get; set; }
        public string AuxiActionName { get; set; }

        private float TimeInHereSpent = 0;

        public void Run(Session session, float elapsedSeconds)
        {
            TimeInHereSpent += elapsedSeconds;
            if (Root.KeyboardNewState.IsKeyDown(Keys.Tab) || session.FastForwarding || (ongoingVoice != null && ongoingVoice.State == SoundState.Stopped && LocalDataStore.AutoMode))
            {
                Dead = true;
                session.QuickEnqueue(new QEndSpeaking());
                session.QuickEnqueue(new QWait(0.05f));
            }
            if (Root.WasMouseLeftClick || Root.WasTouchReleased)
            {
                if (TimeInHereSpent >= 0.3f)
                {
                    session.SpeakingText = null;
                    Dead = true;
                    Root.WasMouseLeftClick = false;
                    Root.WasTouchReleased = false;
                }
            }
        }
    }

    public class QEndSpeaking : QEvent
    {
        public override void Begin(Session session)
        {
            session.SpeakingText = null;
        }
    }
}