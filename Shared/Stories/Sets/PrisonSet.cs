using System.Collections.Generic;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.Sounds;
using Nsnbc.Stories.Scenes;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Sets
{
    public static class PrisonSet
    {
        public static IEnumerable<Script> GetScripts()
        {
            yield return new Script(BookmarkId.R1_Guardhouse_Level, new QEvent[]
            {
                new QPushScene(SceneName.Prison),
                new QSilence(),
                new QSfx(SoundEffectName.SfxWhoosh),
                new QFlyFromCenter(G.CzEn(ArtName.Najdi, ArtName.NajdiEn), 1),
                new QSfx(SoundEffectName.SfxWhoosh),
                new QFlyFromCenter(G.CzEn(ArtName.Cestu, ArtName.CestuEn), 1),
                new QSfx(SoundEffectName.SfxWhoosh),
                new QFlyFromCenter(G.CzEn(ArtName.Ven, ArtName.VenEn), 1),
                new QWait(1, true),
                new QEndFlyouts(),
                new QEqatec("SEEK-A-WAY-OUT: PRISON"),
                new QYouHaveControl(true),
                new QBeginSong(Songname.Gameplay)
            });
        }
    }
}