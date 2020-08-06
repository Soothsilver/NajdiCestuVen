using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Nsnbc.Services;

namespace Nsnbc.Sounds
{
    public class Truesong
    {
        public static Truesong ByName(Songname name)
        {
            return name switch
            {
                Songname.Gameplay => Gameplay,
                Songname.Menu => Menu,
                Songname.Story => Story,
                _ => throw new ArgumentException("Unknown enum value.")
            };
        }
        
        public static Truesong Menu { get; private set; } = null!;
        public static Truesong Story { get; private set; } = null!;
        public static Truesong Gameplay { get; private set; } = null!;

        public SoundEffectReference SoundEffectReference { get; }
        public float VolumeAdjustment { get; }

        private Truesong(SoundEffectReference soundEffect, float volumeAdjustment)
        {
            SoundEffectReference = soundEffect;
            VolumeAdjustment = volumeAdjustment;
        }

        public static void LoadSongs(ContentManager content)
        {
            Menu = new Truesong(PlatformServices.Services.TheBass.LoadSoundEffect("Audio/Music/Adventure2.ogg"), 0.5f);
            Story = new Truesong(PlatformServices.Services.TheBass.LoadSoundEffect("Audio/Music/SmallAdventurers.ogg"), 0.75f);
            Gameplay = new Truesong(PlatformServices.Services.TheBass.LoadSoundEffect("Audio/Music/EthnoAmbience.ogg"), 1);
        }
    }
}