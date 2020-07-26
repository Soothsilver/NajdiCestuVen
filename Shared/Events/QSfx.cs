using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Sounds;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QSfx : QEvent
    {
        private readonly SoundEffectName sfxName;
        private readonly float volume;

        public QSfx(SoundEffectName sfxName, float volume = 1)
        {
            this.sfxName = sfxName;
            this.volume = volume;
        }

        public override void Begin(AirSession airSession)
        {
            Sfxs.Play(sfxName, volume);
        }

        public override void FastForward(AirSession airSession)
        {
            // Skip.
        }
    }
}