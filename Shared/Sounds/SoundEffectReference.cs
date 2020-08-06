using System;
using Nsnbc.Services;

namespace Nsnbc.Sounds
{
    /// <summary>
    /// Represents an instance of a sound effect. The instance cannot be played twice simultaneously.
    /// </summary>
    public abstract class SoundEffectReference
    {
        public abstract bool IsStopped { get; }
        public abstract float Volume { set; }

        public abstract void StopIfPossible();
        public abstract void MakeLooped();
        public abstract void Play();
    }
}