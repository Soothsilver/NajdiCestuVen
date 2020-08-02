using System.Collections.Generic;
using Nsnbc.Events;
using Nsnbc.Sounds;
using Nsnbc.Stories.Sets;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Scenes.Prison
{
    public static class PrisonDialog
    {
        public static IEnumerable<Script> GetScripts()
        {
            yield return new Script(BookmarkId.R1_Guardhouse_Xml_Level, new QEvent[]
            {
                new QPushScene(SceneName.PrisonXML),
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
            yield return new Script(BookmarkId.R1_True_Victory, new QEvent[]
            {  
                new QSilence(),
                new QSfx(SoundEffectName.SfxSuccess),
                new QFlyFromCenter(G.CzEn(ArtName.YouFoundIt, ArtName.YouFoundItEn), 1),
                new QWait(2, true),
                new QEqatec("YOU-FOUND-IT: TECH DEMO"),
                new QEndFlyouts(),
                new QKnownAction(KnownAction.ClearInventory),
                new QPopScene(),
                new QRain(0.05f),
                new QBeginSong(Songname.Story),
                new QSetBackground(ArtName.PotemnelaChodba1),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Left),
                new QSpeak("Tišík", "...Akelo?", ArtName.TisikSpeaking, SpeakerPosition.Right, Voice.B1_Tisik),
            });
        }
    }
}