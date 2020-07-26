using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Nsnbc.Sounds
{
    public class Truesound
    {
        private static SoundEffect SfxDoorHandle = null!;
        private static SoundEffect SfxHarp = null!;
        private static SoundEffect SfxMonsterAppears = null!;
        private static SoundEffect SfxStormBegins = null!;
        private static SoundEffect SfxThunder = null!;
        private static SoundEffect SfxTrezorOpen = null!;
        private static SoundEffect SfxWhoosh = null!;
        private static SoundEffect SfxSuccess = null!;
        private static SoundEffect SfxNumber = null!;
        
        public static SoundEffect Get(SoundEffectName name)
        {
            return soundEffects[name];
        }
        
        private static Dictionary<SoundEffectName, SoundEffect> soundEffects = new Dictionary<SoundEffectName, SoundEffect>();

        public static void LoadSoundEffects(ContentManager content)
        {
            SfxDoorHandle = content.Load<SoundEffect>("Sfx\\DoorHandle");
            SfxHarp = content.Load<SoundEffect>("Sfx\\Harp");
            SfxMonsterAppears = content.Load<SoundEffect>("Sfx\\MonsterAppears");
            SfxStormBegins = content.Load<SoundEffect>("Sfx\\StormBegins");
            SfxThunder = content.Load<SoundEffect>("Sfx\\Thunder");
            SfxTrezorOpen = content.Load<SoundEffect>("Sfx\\TrezorOpen");
            SfxSuccess = content.Load<SoundEffect>("Sfx\\DRAMAT13");
            SfxWhoosh = content.Load<SoundEffect>("Sfx\\Whoosh");
            SfxNumber = content.Load<SoundEffect>("Sfx\\2");

            soundEffects.Add(SoundEffectName.SfxDoorHandle, SfxDoorHandle);
            soundEffects.Add(SoundEffectName.SfxHarp, SfxHarp);
            soundEffects.Add(SoundEffectName.SfxMonsterAppears, SfxMonsterAppears);
            soundEffects.Add(SoundEffectName.SfxStormBegins, SfxStormBegins);
            soundEffects.Add(SoundEffectName.SfxThunder, SfxThunder);
            soundEffects.Add(SoundEffectName.SfxTrezorOpen, SfxTrezorOpen);
            soundEffects.Add(SoundEffectName.SfxWhoosh, SfxWhoosh);
            soundEffects.Add(SoundEffectName.SfxSuccess, SfxSuccess);
            soundEffects.Add(SoundEffectName.SfxNumber, SfxNumber);
        }
    }

    public enum SoundEffectName
    {
        SfxDoorHandle ,
        SfxHarp ,
        SfxMonsterAppears ,
        SfxStormBegins ,
        SfxThunder ,
        SfxTrezorOpen ,
        SfxWhoosh ,
        SfxSuccess ,
        SfxNumber ,
    }
}