using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.SerializableCode;
using Nsnbc.Services;
using Nsnbc.Sounds;
using Nsnbc.Stories.Scenes.Xml;
using Nsnbc.Texts;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QSpeak : QEvent
    {
        private readonly GString speaker;
        private readonly GString text;
        private readonly ArtName sprite;
        private readonly SpeakerPosition position;
        private readonly Voice voice;
        private AuxiliaryAction? auxiliaryAction;

        private QSpeak() // meant for deserializer only
        {
        }
        
        public QSpeak(string speaker, GString text, ArtName sprite, SpeakerPosition position, Voice voice = Voice.Null, AuxiliaryAction? auxiliaryAction = null)
        {
            this.speaker = G.T(speaker);
            this.text = text;
            this.sprite = sprite;
            this.position = position;
            this.voice = voice;
            this.auxiliaryAction = auxiliaryAction;
        }
        public QSpeak(string speaker, string text, ArtName sprite, SpeakerPosition position, Voice voice = Voice.Null, AuxiliaryAction? auxiliaryAction = null)
        : this(speaker, G.T(text), sprite, position, voice, auxiliaryAction)
        {
        }

        public override void Begin(AirSession airSession)
        {
            Sfxs.StopDotting();
            if (position == SpeakerPosition.Left)
            {
                airSession.Session.CurrentLine.SpeakerLeft = sprite;
            }
            else
            {
                airSession.Session.CurrentLine.SpeakerRight = sprite;
            }
            airSession.Session.CurrentLine.SpeakingSpeaker = speaker;
            airSession.Session.CurrentLine.SpeakingText = text;
            airSession.Session.CurrentLine.SpeakerPosition = position;
            airSession.Session.CurrentLine.SpeakingAuxiAction = auxiliaryAction;
            SoundEffectReference? ongoingVoice = null;
           
            if (voice != Voice.Null && Settings.Instance.UseVoices)
            {
                ongoingVoice =  Sfxs.PlayVoice(voice);
            }
            else if (!string.IsNullOrEmpty(speaker.ToString()))
            {
                Sfxs.BeginDotting(text.ToString());
            }
            airSession.ActiveActivities.Add(new SpeakActivity(ongoingVoice));
        }

        public override void FastForward(AirSession airSession)
        {
            // Don't do it. Maybe just do the speaker, though?
        }

        public class AuxiliaryAction
        {
            public GString Caption { get; }
            public Code Effect { get; }

            public AuxiliaryAction(GString caption, Code effect)
            {
                Caption = caption;
                Effect = effect;
            }
        }

        public class SpeakActivity : IQActivity
        {
            private readonly SoundEffectReference? ongoingVoice;
            private float timeInHereSpent;

            public SpeakActivity(SoundEffectReference? soundEffectInstance)
            {
                ongoingVoice = soundEffectInstance;
            }

            public bool Dead { get; private set; }
            public bool Blocking => true;
            
            public void Update(AirSession airSession, float elapsedSeconds)
            {
                timeInHereSpent += elapsedSeconds;
                if (Root.KeyboardNewState.IsKeyDown(Keys.Tab) || airSession.FastForwarding || (ongoingVoice != null && ongoingVoice.IsStopped && Settings.Instance.AutoMode))
                {
                    Dead = true;
                    airSession.QuickEnqueue(new QEndSpeaking());
                    airSession.QuickEnqueue(new QWait(0.05f));
                }
                else if (Root.KeyboardNewState.IsKeyDown(Keys.F3))
                {
                    Dead = true;
                    airSession.QuickEnqueue(new QEndSpeaking());
                }
                if (Root.WasMouseLeftClick || Root.WasTouchReleased)
                {
                    if (timeInHereSpent >= 0.3f)
                    {
                        airSession.Session.CurrentLine.SpeakingText = null;
                        Dead = true;
                        Root.WasMouseLeftClick = false;
                        Root.WasTouchReleased = false;
                    }
                }
            }
        }

        public static QSpeak Quick(string text)
        {
            return new QSpeak("", text,ArtName.Null, SpeakerPosition.Left);
        }

        public static QEvent From(string speaker, Pose speakerPose, string sentence)
        {
            return new QSpeak(speaker, sentence, XmlCharacters.FindArt(speaker, speakerPose.ToString().ToLower()), SpeakerPosition.Left);
        }
    }
}