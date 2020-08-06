using System;
using Nsnbc.Services;
using Un4seen.Bass;

namespace Nsnbc.Sounds.BassNet
{
    public class GreaterBass : LesserBass
    {
        public GreaterBass()
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
        }
        public override SoundEffectReference PlaySoundEffect(string filename, float volume)
        {
            int stream = PlatformServices.Services.LoadBassFileAsStream(filename);
            Bass.BASS_ChannelSetAttribute(stream, BASSAttribute.BASS_ATTRIB_VOL, volume);
            Bass.BASS_ChannelPlay(stream, true);
            return new BassSoundEffectReference(stream);
        }

        public override SoundEffectReference LoadSoundEffect(string filename)
        {
            int stream = PlatformServices.Services.LoadBassFileAsStream(filename);
            return new BassSoundEffectReference(stream);
        }
    }
}