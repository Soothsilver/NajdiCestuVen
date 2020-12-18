using System.Collections.Generic;
using Nsnbc.Events;
using Nsnbc.Sounds;
using Nsnbc.Stories.Sets;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Scenes.Courtyard
{
    public class CourtyardDialog
    {
        public static IEnumerable<Script> GetScripts()
        {
            yield return new Script(BookmarkId.R2_Courtyard, new QEvent[]
            {
                new QPushScene(SceneName.CourtyardXML),
                new QEqatec("SEEK-A-WAY-OUT: COURTYARD"),
                new QEnterGameplay(), 
            });
            yield return new Script(BookmarkId.R2_Victory, new QEvent[]
            {  
                new QSilence(),
                new QSfx(SoundEffectName.Success),
                new QFlyFromCenter(G.CzEn(ArtName.YouFoundIt, ArtName.YouFoundItEn), 1),
                new QWait(2, true),
                new QEqatec("YOU-FOUND-IT: COURTYARD"),
                new QEndFlyouts(),
                new QKnownAction(KnownAction.ClearInventory),
                new QPopScene(),
                new QRain(0.05f),
                new QBeginSong(Songname.Story),
                new QSetBackground(ArtName.Darkness),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Left),
                new QSpeak("Programátor", "Dostali jste se na konec technického dema 2!", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.Null),
                new QSpeak("Programátor", "Možná byste chtěli pokračovat dál?", ArtName.AkelaThinking, SpeakerPosition.Left, Voice.Null),
                new QSpeak("Programátor", "V tom případě se prosím podělte o vaše zkušenosti a pomozte tak s vývojem hry!", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.Null),
                new QSpeak("Programátor", "V každém případě děkujeme, že jste si zahráli toto technické demo, a zatím se mějte!", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.Null),
                new QReturnToMenu()
            });
        }
    }
}