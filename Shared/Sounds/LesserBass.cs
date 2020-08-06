namespace Nsnbc.Sounds
{
    public abstract class LesserBass
    {
        public abstract SoundEffectReference PlaySoundEffect(string filename, float volume);
        public abstract SoundEffectReference LoadSoundEffect(string filename);
    }
}