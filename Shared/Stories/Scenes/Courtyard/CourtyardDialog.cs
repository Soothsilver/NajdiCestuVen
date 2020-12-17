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
                new QSetBackground(ArtName.PotemnelaChodba1),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Left),
                new QSpeak("Tišík", "...Akelo?", ArtName.TisikSpeaking, SpeakerPosition.Right, Voice.B1_Tisik),
            });
        }
    }
}