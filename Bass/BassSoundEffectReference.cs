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

        public override float Volume
        {
            set
            {
                Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, value);
            }
        }

        public override void StopIfPossible() => Bass.BASS_ChannelStop(stream);
        public override void MakeLooped()
        {
            Bass.BASS_ChannelFlags(stream, BASSFlag.BASS_SAMPLE_LOOP, BASSFlag.BASS_SAMPLE_LOOP);
        }

        public override void Play()
        {
            Bass.BASS_ChannelPlay(stream, true);
        }
    }
}