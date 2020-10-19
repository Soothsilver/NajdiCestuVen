﻿using System.Collections.Generic;
using Nsnbc.Events;
using Nsnbc.Sounds;
using Nsnbc.Stories.Scenes.Prologue;
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
                new QEqatec("SEEK-A-WAY-OUT: PRISON"),
                new QEnterGameplay(), 
            });
            yield return new Script(BookmarkId.R1_True_Victory, new QEvent[]
            {  
                new QSilence(),
                new QSfx(SoundEffectName.Success),
                new QFlyFromCenter(G.CzEn(ArtName.YouFoundIt, ArtName.YouFoundItEn), 1),
                new QWait(2),
                new QEqatec("YOU-FOUND-IT: PRISON"),
                new QEndFlyouts(),
                new QKnownAction(KnownAction.ClearInventory),
                new QPopScene(),
                new QRain(0.05f),
                new QBeginSong(Songname.Story),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Left),
                new QSpeak("", "...detektor lži?", ArtName.TisikSpeaking, SpeakerPosition.Right, Voice.B1_Tisik),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right),
                new QSpeak("Programátor", "A nyní se přesuneme k druhé místnosti!", ArtName.AkelaExcited, SpeakerPosition.Left),
                new QGoToBookmark(BookmarkId.R2_Courtyard), 
            });
        }
    }
}