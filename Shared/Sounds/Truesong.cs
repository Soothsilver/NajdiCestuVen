using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

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

        public SoundEffect SoundEffect { get; }
        public float VolumeAdjustment { get; }

        private Truesong(SoundEffect soundEffect, float volumeAdjustment)
        {
            SoundEffect = soundEffect;
            VolumeAdjustment = volumeAdjustment;
        }

        public static void LoadSongs(ContentManager content)
        {
            Menu = new Truesong(content.Load<SoundEffect>("Music\\Adventure2"), 0.5f);
            Story = new Truesong(content.Load<SoundEffect>("Music\\SmallAdventurers"), 0.75f);
            Gameplay = new Truesong(content.Load<SoundEffect>("Music\\EthnoAmbience"), 1);
        }
    }
}