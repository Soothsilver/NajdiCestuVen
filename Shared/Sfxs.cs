using System;
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

        public static void LoadContent(ContentManager content)
        {
            MusicMenu = content.Load<SoundEffect>("Music\\SadnessCombo");
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
            SfxTypeBlip = content.Load<SoundEffect>("Sfx\\LowBlipEdit");
            blip = SfxTypeBlip.CreateInstance();
            blip.IsLooped = false;
            blip.Volume = 0.2f;
        }
        
        public static void Play(SoundEffect effect)
        {
            var newEffect = effect.CreateInstance();
            newEffect.IsLooped = false;
            newEffect.Volume = 0.5f;
            newEffect.Play();
        }
        public static void BeginRain(float volume)
        {
            rainSfxInstance?.Stop(true);
            rainSfxInstance = SfxRain.CreateInstance();
            rainSfxInstance.IsLooped = true;
            rainSfxInstance.Volume = volume;
            rainSfxInstance.Play();
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
        }

        public static void BeginDotting()
        {
            blip.Stop();
            nextWhen = DateTime.Now;
            endWhen = DateTime.Now.AddSeconds(0.2f);
        }

        private static SoundEffectInstance blip;
        private static DateTime endWhen;
        private static DateTime nextWhen;

        public static void Update()
        {
            if (endWhen > DateTime.Now)
            {
                if (DateTime.Now > nextWhen)
                {
                    blip.Stop();
                    blip.Play();
                    nextWhen = DateTime.Now.AddSeconds(0.02f);
                }
            }
        }
    }
}