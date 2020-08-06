using System;
using System.Collections.Concurrent;
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
        private static float musicFileInherentVolumeModifier;
        private static float blipInherentVolumeModifier;
        private static SoundEffectReference? musicSfxInstance;
        private static SoundEffectReference rainSfxInstance = null!;
        private static SoundEffectReference blip = null!;

        /// <summary>
        /// True if the game window is the foreground focused Windows application.
        /// </summary>
        public static bool WindowActive { get; set; } = true;

        public static void LoadSfxs()
        {
            // Truesound.LoadSoundEffects(content);
            // sfxRain = content.Load<SoundEffect>("Sfx\\Rain");
            // sfxTypeBlip = content.Load<SoundEffect>("Sfx\\PhoenixBlip");
            rainSfxInstance = PlatformServices.Services.TheBass.LoadSoundEffect("Audio/Sfx/Rain.ogg");
            blip = PlatformServices.Services.TheBass.LoadSoundEffect("Audio/Sfx/PhoenixBlip.ogg");
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
        
        public static SoundEffectReference Play(SoundEffectName effect, float volume = 1)
        {
            var newEffect = PlatformServices.Services.TheBass.PlaySoundEffect("Audio/Sfx/" + effect.ToString() + ".ogg", Settings.Instance.MasterVolume * Settings.Instance.SfxVolume * volume);
            return newEffect;
        }
        public static void BeginRain(float volume)
        {
            rainSfxInstance.StopIfPossible();
            rainSfxInstance.MakeLooped();
            rainSfxInstance.Volume = Settings.Instance.MasterVolume * Settings.Instance.SfxVolume * (volume * 0.9f);
            rainSfxInstance.Play();
        }

        private static SoundEffectReference? lastVoice;
        public static SoundEffectReference PlayVoice(Voice voice)
        {
            lastVoice?.StopIfPossible();
            var newVoice = PlatformServices.Services.TheBass.PlaySoundEffect("Audio/VFX/" + voice.ToString() + ".ogg",  Settings.Instance.VoiceVolume * Settings.Instance.MasterVolume * (voice.ToString().EndsWith("Skok") ? 0.6f : 1));
            return lastVoice = newVoice;
        }

        
        public static void BeginSong(Songname song)
        {
            Silence();
            Truesong truesong = Truesong.ByName(song);
            musicFileInherentVolumeModifier = truesong.VolumeAdjustment;
            musicSfxInstance = truesong.SoundEffectReference;
            musicSfxInstance.Volume = musicFileInherentVolumeModifier * Settings.Instance.MasterVolume * Settings.Instance.MusicVolume;
            musicSfxInstance.MakeLooped();
            musicSfxInstance.Play();
            UpdateVolumes();
        }

        public static void Silence()
        {
            rainSfxInstance?.StopIfPossible();
            musicSfxInstance?.StopIfPossible();
            lastVoice?.StopIfPossible();
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
                    blip.StopIfPossible();
                    blip.Play();
                    nextWhen = DateTime.Now.AddMilliseconds(Pauses[0]);
                    Pauses.RemoveAt(0);
                }
            }
        }
    }
}