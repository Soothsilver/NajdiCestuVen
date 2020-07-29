using System.Collections.Generic;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.Sounds;
using Nsnbc.Stories.Scenes;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Sets
{
    public static class PrisonDialog
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
            yield return new Script(BookmarkId.R1_G1_Table, new QEvent[]
            {
                new QSpeak("Tišík", "Wow, takovéhle zařízení jsem ještě neviděl.", ArtName.TisikPointing, SpeakerPosition.Left),
                new QSpeak("Vypravěč", "To je stůl. Už jsi ho viděl, Tišíku..", ArtName.SkokNastvany, SpeakerPosition.Left), 
                new QSpeak("Tišík", "Samozřejmě jsem myslel na ten kovový kvádr na něm, trumbero!", ArtName.TisikAngry, SpeakerPosition.Left),
                new QSpeak("Vědátor", "Mě to něco připomíná. Možná bychom se měli podívat zblízka.", ArtName.VedatorThinking, SpeakerPosition.Left),
                new QPushScene(SceneName.R1_Table), 
            });
            yield return new Script(BookmarkId.R1_G1_Gate, new QEvent[]
            {
                new QSpeak("Tišík", "Něco Něco TODO TODO.", ArtName.TisikPointing, SpeakerPosition.Left), 
            });
            yield return new Script(BookmarkId.R1_G1_Electricity, new QEvent[]
            {
                new QSpeak("Tišík", "Něco Něco TODO TODO.", ArtName.TisikPointing, SpeakerPosition.Left),
            });
            yield return new Script(BookmarkId.R1_G1_Fridge, new QEvent[]
            {
                new QSpeak("Tišík", "Něco Něco TODO TODO.", ArtName.TisikPointing, SpeakerPosition.Left),
                new QPushScene(SceneName.R1_Fridge), 
            });
            yield return new Script(BookmarkId.R1_G1_GreenDoor, new QEvent[]
            {
                new QSpeak("Tišík", "Něco Něco TODO TODO.", ArtName.TisikPointing, SpeakerPosition.Left),
            });
            yield return new Script(BookmarkId.R1_G2_PravdaLaska, new QEvent[]
            {
                new QSetBackground(ArtName.Obraz1),
                new QSpeak("Tišík", "Něco Něco TODO TODO.", ArtName.TisikPointing, SpeakerPosition.Left),
                new QPopScene(), 
            });
            yield return new Script(BookmarkId.R1_G2_Svitek, new QEvent[]
            {
                new QSetBackground(ArtName.Obraz2),
                new QSpeak("Tišík", "Něco Něco TODO TODO.", ArtName.TisikPointing, SpeakerPosition.Left),
                new QPopScene(), 
            });
            yield return new Script(BookmarkId.R1_G2_Svitek2, new QEvent[]
            {
                new QSetBackground(ArtName.Obraz2),
                new QSpeak("Tišík", "Něco Něco 2 2 TODO TODO.", ArtName.TisikPointing, SpeakerPosition.Left),
                new QPopScene(), 
            });
        }
    }
}