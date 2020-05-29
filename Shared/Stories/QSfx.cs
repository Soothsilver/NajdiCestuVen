using Microsoft.Xna.Framework.Audio;

namespace Nsnbc.Android.Stories
{
    public class QSfx : QEvent
    {
        private readonly SoundEffect sfxThunder;
        private readonly float volume;

        public QSfx(SoundEffect sfxThunder, float volume = 1)
        {
            this.sfxThunder = sfxThunder;
            this.volume = volume;
        }

        public override void Begin(Session session)
        {
            Sfxs.Play(sfxThunder, volume);
        }
    }
}