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
        public abstract void StopIfPossible();
    }
}