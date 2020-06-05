using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Nsnbc.Android
{
    public class Sfxs
    {
        public static SoundEffect MusicMenu;
        public static SoundEffect MusicStory;
        public static SoundEffect MusicGameplay;
        private static SoundEffectInstance musicSfxInstance;
        private static SoundEffectInstance rainSfxInstance;

        public static SoundEffect SfxDoorHandle;
        public static SoundEffect SfxHarp;
        public static SoundEffect SfxMonsterAppears;
        public static SoundEffect SfxRain;
        public static SoundEffect SfxStormBegins;
        public static SoundEffect SfxThunder;
        public static SoundEffect SfxTrezorOpen;
        public static SoundEffect SfxWhoosh;
        public static SoundEffect SfxSuccess;
        public static SoundEffect SfxNumber;
        public static SoundEffect SfxTypeBlip;
        public static Dictionary<Voice, SoundEffect> Voices { get; set; } = new Dictionary<Voice, SoundEffect>();

        public static void LoadContent(ContentManager content)
        {
            MusicMenu = content.Load<SoundEffect>("Music\\Adventure2");
            MusicStory = content.Load<SoundEffect>("Music\\SmallAdventurers");
            MusicGameplay = content.Load<SoundEffect>("Music\\EthnoAmbience");

            SfxDoorHandle = content.Load<SoundEffect>("Sfx\\DoorHandle");
            SfxHarp = content.Load<SoundEffect>("Sfx\\Harp");
            SfxMonsterAppears = content.Load<SoundEffect>("Sfx\\MonsterAppears");
            SfxRain = content.Load<SoundEffect>("Sfx\\Rain");
            SfxStormBegins = content.Load<SoundEffect>("Sfx\\StormBegins");
            SfxThunder = content.Load<SoundEffect>("Sfx\\Thnder");
            SfxTrezorOpen = content.Load<SoundEffect>("Sfx\\TrezorOpen");
            SfxSuccess = content.Load<SoundEffect>("Sfx\\DRAMAT13");
            SfxWhoosh = content.Load<SoundEffect>("Sfx\\Whoosh");
            SfxNumber = content.Load<SoundEffect>("Sfx\\2");
            SfxTypeBlip = content.Load<SoundEffect>("Sfx\\PhoenixBlip");
            foreach (Voice art in typeof(Voice).GetEnumValues())
            {
                try
                {
                    Voices.Add(art, content.Load<SoundEffect>("Vfx\\" + art));
                }
                catch 
                {
                    
                }
            }
            blip = SfxTypeBlip.CreateInstance();
            blip.IsLooped = false;
            blip.Volume = 0.08f;
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
            rainSfxInstance = SfxRain.CreateInstance();
            rainSfxInstance.IsLooped = true;
            rainSfxInstance.Volume = volume * 0.3f;
            rainSfxInstance.Play();
        }

        private static SoundEffectInstance lastVoice = null;
        public static SoundEffectInstance PlayVoice(Voice voice)
        {
            lastVoice?.Stop();
            var newEffect = lastVoice = Voices[voice].CreateInstance();
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
            pauses.Clear();
            dotting = false;
        }

        public static void BeginDotting(string line)
        {
            StopDotting();
            if (LocalDataStore.BeepingMode)
            {
                dotting = true;
                string[] words = line.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                pauses.Add(50);
                foreach (string word in words)
                {
                    //  int size = Math.Min(word.Length / 3, 4);
                    //   for (int i = 0; i < size; i++)
                    //  {
                    pauses.Add((word.Contains(".") || word.Contains("?") || word.Contains(",")) ? 200 : 50);
                    //   }
                    //  pauses.Add(150);

                }

                nextWhen = DateTime.Now;
            }
        }

        private static List<int> pauses = new List<int>();
        private static SoundEffectInstance blip;
        private static DateTime endWhen;
        private static DateTime nextWhen;
        private static bool dotting = false;

        public static void Update()
        {
            if (dotting)
            {
                if (DateTime.Now >= nextWhen)
                {
                    if (pauses.Count == 1)
                    {
                        pauses.Clear();
                        dotting = false;
                        return;
                    }
                    blip.Stop();
                    blip.Play();
                    nextWhen = DateTime.Now.AddMilliseconds(pauses[0]);
                    pauses.RemoveAt(0);
                }
            }
        }

       
    }
}