using Un4seen.Bass;

namespace Nsnbc.Sounds.BassNet
{
    public class BassSoundEffectReference : SoundEffectReference
    {
        private readonly int stream;

        public BassSoundEffectReference(int stream)
        {
            this.stream = stream;
        }

        public override bool IsStopped => Bass.BASS_ChannelIsActive(stream) != BASSActive.BASS_ACTIVE_PLAYING;
        public override void StopIfPossible() => Bass.BASS_ChannelStop(stream);
    }
}