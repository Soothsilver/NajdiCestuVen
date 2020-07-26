using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Nsnbc.PostSharp;
using Nsnbc.Services;

namespace Nsnbc.Sounds
{
    [Trace]
    public static class Sfxs
    {
        private static SoundEffectInstance? musicSfxInstance;
        private static float musicFileInherentVolumeModifier;
        private static float blipInherentVolumeModifier;
        private static SoundEffectInstance? rainSfxInstance;


        
        private static SoundEffect sfxTypeBlip = null!;
        private static SoundEffect sfxRain = null!;
        public static Dictionary<Voice, SoundEffect> Voices { get; } = new Dictionary<Voice, SoundEffect>();

        /// <summary>
        /// True if the game window is the foreground focused Windows application.
        /// </summary>
        public static bool WindowActive { get; set; } = true;

        public static void LoadVoice(ContentManager content, Voice art)
        {                
            Voices.Add(art, content.Load<SoundEffect>("Vfx\\" + art));
        }

        public static void LoadSfxs(ContentManager content)
        {
            Truesound.LoadSoundEffects(content);
            sfxRain = content.Load<SoundEffect>("Sfx\\Rain");
            sfxTypeBlip = content.Load<SoundEffect>("Sfx\\PhoenixBlip");
            blip = sfxTypeBlip.CreateInstance();
            blip.IsLooped = false;
            blipInherentVolumeModifier = 0.08f;
            UpdateVolumes();
        }

        public static void UpdateVolumes()
        {
            if (blip != null)
            {
                blip.Volume = blipInherentVolumeModifier * Settings.Instance.MasterVolume;
            }

            if (musicSfxInstance != null)
            {
                if (WindowActive || !Settings.Instance.PauseMusicWhileInactive)
                {
                    musicSfxInstance.Volume = musicFileInherentVolumeModifier * Settings.Instance.MasterVolume * Settings.Instance.MusicVolume;
                }
                else
                {
                    musicSfxInstance.Volume = 0;
                }
            }
        }

        public static void LoadMusic(ContentManager content)
        {
            Truesong.LoadSongs(content);
        }
        
        public static SoundEffectInstance Play(SoundEffectName effect, float volume = 1)
        {
            var newEffect = Truesound.Get(effect).CreateInstance();
            newEffect.IsLooped = false;
            newEffect.Volume = Settings.Instance.MasterVolume * Settings.Instance.SfxVolume * volume;
            newEffect.Play();
            return newEffect;
        }
        public static void BeginRain(float volume)
        {
            rainSfxInstance?.Stop(true);
            rainSfxInstance = sfxRain.CreateInstance();
            rainSfxInstance.IsLooped = true;
            rainSfxInstance.Volume = Settings.Instance.MasterVolume * Settings.Instance.SfxVolume * (volume * 0.9f);
            rainSfxInstance.Play();
        }

        private static SoundEffectInstance? lastVoice;
        public static SoundEffectInstance PlayVoice(Voice voice)
        {
            lastVoice?.Stop();
            SoundEffectInstance? newEffect = lastVoice = Voices[voice].CreateInstance();
            newEffect.IsLooped = false;
            newEffect.Volume = Settings.Instance.VoiceVolume * Settings.Instance.MasterVolume * (voice.ToString().EndsWith("Skok") ? 0.6f : 1);
            newEffect.Play();
            return newEffect;
            
        }

        
        public static void BeginSong(Truesong song)
        {
            Silence();
            musicFileInherentVolumeModifier = song.VolumeAdjustment;
            musicSfxInstance = song.SoundEffect.CreateInstance();
            musicSfxInstance.IsLooped = true;
            UpdateVolumes();
            musicSfxInstance.Play();
        }

        public static void Silence()
        {
            rainSfxInstance?.Stop(true);
            musicSfxInstance?.Stop(true);
            lastVoice?.Stop(true);
            StopDotting();
        }

        public static void StopDotting()
        {
            Pauses.Clear();
            dotting = false;
        }

        public static void BeginDotting(string line)
        {
            StopDotting();
            if (Settings.Instance.BeepUnvoicedLines)
            {
                dotting = true;
                string[] words = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                Pauses.Add(50);
                foreach (string word in words)
                {
                    //  int size = Math.Min(word.Length / 3, 4);
                    //   for (int i = 0; i < size; i++)
                    //  {
                    Pauses.Add((word.Contains(".") || word.Contains("?") || word.Contains(",")) ? 200 : 50);
                    //   }
                    //  pauses.Add(150);

                }

                nextWhen = DateTime.Now;
            }
        }

        private static readonly List<int> Pauses = new List<int>();
        private static SoundEffectInstance blip = null!;
        private static DateTime nextWhen;
        private static bool dotting;

        [Trace(AttributeExclude = true)]
        public static void Update()
        {
            if (dotting)
            {
                if (DateTime.Now >= nextWhen)
                {
                    if (Pauses.Count == 1)
                    {
                        Pauses.Clear();
                        dotting = false;
                        return;
                    }
                    blip.Stop();
                    blip.Play();
                    nextWhen = DateTime.Now.AddMilliseconds(Pauses[0]);
                    Pauses.RemoveAt(0);
                }
            }
        }


 
    }
}