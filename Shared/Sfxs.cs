using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Nsnbc.PostSharp;

namespace Nsnbc.Android
{
    [Trace]
    public static class Sfxs
    {
        public static SoundEffect MusicMenu = null!;
        public static SoundEffect MusicStory = null!;
        public static SoundEffect MusicGameplay = null!;
        private static SoundEffectInstance? musicSfxInstance;
        private static SoundEffectInstance? rainSfxInstance;

        public static SoundEffect SfxDoorHandle = null!;
        public static SoundEffect SfxHarp = null!;
        public static SoundEffect SfxMonsterAppears = null!;
        private static SoundEffect sfxRain = null!;
        public static SoundEffect SfxStormBegins = null!;
        public static SoundEffect SfxThunder = null!;
        public static SoundEffect SfxTrezorOpen = null!;
        public static SoundEffect SfxWhoosh = null!;
        public static SoundEffect SfxSuccess = null!;
        public static SoundEffect SfxNumber = null!;
        private static SoundEffect sfxTypeBlip = null!;
        public static Dictionary<Voice, SoundEffect> Voices { get; } = new Dictionary<Voice, SoundEffect>();
        
        public static void LoadVoice(ContentManager content, Voice art)
        {                
            Voices.Add(art, content.Load<SoundEffect>("Vfx\\" + art));
        }

        public static void LoadSfxs(ContentManager content)
        { 
            SfxDoorHandle = content.Load<SoundEffect>("Sfx\\DoorHandle");
            SfxHarp = content.Load<SoundEffect>("Sfx\\Harp");
            SfxMonsterAppears = content.Load<SoundEffect>("Sfx\\MonsterAppears");
            sfxRain = content.Load<SoundEffect>("Sfx\\Rain");
            SfxStormBegins = content.Load<SoundEffect>("Sfx\\StormBegins");
            SfxThunder = content.Load<SoundEffect>("Sfx\\Thnder");
            SfxTrezorOpen = content.Load<SoundEffect>("Sfx\\TrezorOpen");
            SfxSuccess = content.Load<SoundEffect>("Sfx\\DRAMAT13");
            SfxWhoosh = content.Load<SoundEffect>("Sfx\\Whoosh");
            SfxNumber = content.Load<SoundEffect>("Sfx\\2");
            sfxTypeBlip = content.Load<SoundEffect>("Sfx\\PhoenixBlip");
            blip = sfxTypeBlip.CreateInstance();
            blip.IsLooped = false;
            blip.Volume = 0.08f;
        }

        public static void LoadMusic(ContentManager content)
        {
            MusicMenu = content.Load<SoundEffect>("Music\\Adventure2");
            MusicStory = content.Load<SoundEffect>("Music\\SmallAdventurers");
            MusicGameplay = content.Load<SoundEffect>("Music\\EthnoAmbience");
        }
        
        public static SoundEffectInstance Play(SoundEffect effect, float volume = 1)
        {
            var newEffect = effect.CreateInstance();
            newEffect.IsLooped = false;
            newEffect.Volume = 0.5f * volume;
            newEffect.Play();
            return newEffect;
        }
        public static void BeginRain(float volume)
        {
            rainSfxInstance?.Stop(true);
            rainSfxInstance = sfxRain.CreateInstance();
            rainSfxInstance.IsLooped = true;
            rainSfxInstance.Volume = volume * 0.3f;
            rainSfxInstance.Play();
        }

        private static SoundEffectInstance? lastVoice;
        public static SoundEffectInstance PlayVoice(Voice voice)
        {
            lastVoice?.Stop();
            SoundEffectInstance? newEffect = lastVoice = Voices[voice].CreateInstance();
            newEffect.IsLooped = false;
            newEffect.Volume = voice.ToString().EndsWith("Skok") ? 0.6f : 1;
            newEffect.Play();
            return newEffect;
            
        }

        
        public static void BeginSong(SoundEffect song)
        {
            Silence();
            musicSfxInstance = song.CreateInstance();
            musicSfxInstance.IsLooped = true;
            musicSfxInstance.Volume = 0.2f;
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
            if (LocalDataStore.BeepingMode)
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