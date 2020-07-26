﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.Sounds;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Sets
{
    public static class TechDemo
    {
        public static IEnumerable<Script> CreateScripts()
        {
            yield return new Script(BookmarkId.TechDemoStart, new QEvent[]
            {
                new QSetBackground(ArtName.Exterior),
                new QBeginSong(Songname.Story),
                new QZoomInto(new Rectangle(115, 226, 600, 337), 12),
                new QWait(2),
                new QSpeak("Akela", "Vlčata! Vítejte na vícedenní výpravě v Chatě teroru!", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.A1_Akela),
                new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left),
                new QSpeak("Vlčata", "Jupí!!!", ArtName.TripleHappy, SpeakerPosition.Right, Voice.A2_TriJoin),
                new QSpeak("Vědátor", "Počkat... chatě teroru?", ArtName.VedatorStrach, SpeakerPosition.Right, Voice.A3_Ved),
                new QPopScene(), 
                new QPushScene(SceneName.TechDemo),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right),
                new QSpeak("Akela", "Ano, přesně tak. Majitel této chaty zemřel před sedmi lety.", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.A4_Akela),
                new QSpeak("Akela", "A to přímo v této místnosti, kde jeho tělo po sedmi dnech našel soused.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A5_Akela),
                new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left),
                new QSpeak("Tišík", "Co se mu s-stalo?", ArtName.TisikStrach, SpeakerPosition.Right, Voice.A6_Tisik),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right),
                new QSpeak("Akela", "To nikdo neví, ale místní říkají, že za života to byl zlý a nenávistný člověk.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A7_Akela),
                new QSpeak("Akela", "A že jeho duch stále bloudí po tomto pozemku.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A8_Akela),
                new QSpeak("Akela", "A hledá nové způsoby, jak se pomstít všem, kdo ho přežili.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A9_Akela),
                new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left),
                new QWait(1),
                new QSpeak("Akela", "...jo, a proto byl účastnický poplatek za tuto výpravu tak malý.", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.A10_Akela),
                new QKnownAction(KnownAction.SwitchToDrizzle), 
                new QRain(0.05f),
                new QSpeak("Akela", "Každopádně, můžete si tady teď rozbalit věci, já půjdu ven připravit hru, a za chvilku se pro vás vrátím.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A11_Akela),
                new QSpeak("Akela", "Do té doby vás má na starosti Skok.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A12_Akela),
                new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left),
                new QSpeak("Tišík", "Um... Akelo? Začíná pršet.", ArtName.TisikPointing, SpeakerPosition.Right, Voice.A13_Tisik),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right),
                new QSpeak("Akela", "Hmm... skutečně? Mno, jak říká skautský zákon, skaut je nepromokavý! Připravte se na hru v dešti!", ArtName.AkelaThinking, SpeakerPosition.Left, Voice.A14_Akela),
                new QSpeak("Akela", "Tak jo, Skoku, máš vedení. Mějte se, jsem zpátky za sedm minut, rozbijte toho co nejmíň. Akela out.", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.A16_Akela),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right),
                new QWait(1),
                new QSetBackground(ArtName.Darkness),
                new QWait(0.1f),
                new QSetBackground(ArtName.Thunderbolt),
                new QSfx(SoundEffectName.SfxThunder),
                new QWait(0.2f),
                new QSetBackground(ArtName.Darkness),
                new QWait(2f),
                new QSpeak("", "O třicet minut později.", ArtName.Null, SpeakerPosition.Left, Voice.A17a_Tisik),
                new QPopScene(), 
                new QRain(0.1f),
                new QSfx(SoundEffectName.SfxStormBegins, 0.25f),
                new QWait(2f),
                new QSpeak("Vědátor", "Deštík, Tišíku, jo? Tohle je blesková bouře, jakou jsem ještě neviděl.", ArtName.VedatorSpeaking, SpeakerPosition.Left, Voice.A18_Ved),
                new QSpeak("Tišík", "V tomhle přece nemůže chtít, abychom šli ven!", ArtName.TisikAngry, SpeakerPosition.Left, Voice.A19_Tisik),
                new QSpeak("Skok", "Je to nebezpečný i pro něj. Už měl dávno být zpátky.", ArtName.SkokNastvany, SpeakerPosition.Left, Voice.A20_Skok),
                new QSpeak("Tišík", "Akela?", ArtName.TisikThinking, SpeakerPosition.Left, Voice.A21_Tisik),
                new QSpeak("Skok", "Jo. Půjdu se podívat na zápraží.", ArtName.SkokMluvici, SpeakerPosition.Left, Voice.A22_Skok),
                new QSfx(SoundEffectName.SfxDoorHandle),
                new QSpeak("", "Skok se pokusí odemknout dveře, ale jsou zamčené.", ArtName.Null, SpeakerPosition.Left, Voice.Silence),
                new QSpeak("Skok", "On... nás tady zamknul?", ArtName.SkokNastvany, SpeakerPosition.Left, Voice.A23_Skok),
                new QSpeak("Tišík", "Říkal, že máme zůstat v téhle místnosti.", ArtName.TisikSpeaking, SpeakerPosition.Left, Voice.A24_Ved),
                new QSpeak("Skok", "Ale zamknout dveře?", ArtName.SkokThinking, SpeakerPosition.Left, Voice.A25_Skok),
                new QSpeak("Tišík", "Myslíte, že se mu něco stalo?", ArtName.TisikThinking, SpeakerPosition.Left, Voice.A26_Tisik),
                new QSpeak("Skok", "Třeba do něj uhodil blesk...?", ArtName.SkokThinking, SpeakerPosition.Left, Voice.A27_Skok),
                new QSpeak("Vědátor", "Nebo... možná ten duch...?", ArtName.VedatorStrach, SpeakerPosition.Left, Voice.A28_Ved),
                new QWait(1f),
                new QSpeak("Tišík", "Musíme Akelu najít. Co když se mu fakt něco stalo?", ArtName.TisikDetermined, SpeakerPosition.Left, Voice.A29_Tisik),
                new QSpeak("Skok", "Souhlasím. A jako první tedy musíme otevřít tyto dveře.", ArtName.SkokMluvici, SpeakerPosition.Left, Voice.A30_Skok),
                new QSpeak("Skok", "Prohledejme místnost. Někde tady musí být náhradní klíč.", ArtName.SkokMluvici, SpeakerPosition.Left, Voice.A31_Skok),
                new QEndSpeaking(),
                new QBookmark(BookmarkId.TechDemo_Level),
                new QSilence(),
                new QSfx(SoundEffectName.SfxWhoosh),
                new QFlyFromCenter(G.CzEn(ArtName.Najdi, ArtName.NajdiEn), 1),
                new QSfx(SoundEffectName.SfxWhoosh),
                new QFlyFromCenter(G.CzEn(ArtName.Cestu, ArtName.CestuEn), 1),
                new QSfx(SoundEffectName.SfxWhoosh),
                new QFlyFromCenter(G.CzEn(ArtName.Ven, ArtName.VenEn), 1),
                new QWait(1, true),
                new QEndFlyouts(),
                new QEqatec("SEEK-A-WAY-OUT: TECH DEMO"),
                new QYouHaveControl(true), 
                new QBeginSong(Songname.Gameplay),
                new QRain(0.05f)
            });
            yield return new Script(BookmarkId.R1_Victory, new QEvent[]
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
                new QSpeak("Vědátor", "Možná je na záchodě?", ArtName.VedatorThinking, SpeakerPosition.Right, Voice.B2_Ved),
                new QSpeak("Skok", "To těžko. Akelovi to nikdy netrvá víc než sedm minut.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B3_Skok),
                new QSpeak("Vědátor", "Takže... stále venku?", ArtName.VedatorThinking, SpeakerPosition.Right, Voice.B4_Ved),
                new QSpeak("Skok", "Může být.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B5_Skok),
                new QSpeak("", "Připravili jsme se k otevření hlavních venkovních dveří.", ArtName.Null, SpeakerPosition.Right, Voice.B6_Tisik),
                new QSpeak("Tišík", "Na tři?", ArtName.TisikDetermined, SpeakerPosition.Right, Voice.B7_Tisik),
                new QSpeak("Vědátor", "Na tři.", ArtName.VedatorSpeaking, SpeakerPosition.Right, Voice.B8_Ved),
                new QSpeak("Skok", "Raz --", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B9_Skok),
                new QSpeak("Skok", "Dva --", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B10_Skok),
                new QSpeak("Skok", "Tři.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B11_Skok),
                new QSetBackground(ArtName.PotemnelaChodba2),
                new QSfx(SoundEffectName.SfxDoorHandle),
                new QRain(0.075f),
                new QSpeak("", "Skok vezme za kliku a dveře se otevřou. Venku lije hromový déšť--", ArtName.Null, SpeakerPosition.Right, Voice.B12_Tisik),
                new QSpeak("Akela", "Baf!", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.B13_Akela),
                new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left),
                new QSpeak("Vlčata", "Aah!", ArtName.TripleFear, SpeakerPosition.Right, Voice.B14_Tri),
                new QSpeak("Tišík", "Akelo!", ArtName.TisikAngry, SpeakerPosition.Right, Voice.B15_Tisik),
                new QSpeak("Vědátor", "Báli jsme se!", ArtName.VedatorStrach, SpeakerPosition.Right, Voice.B16_Ved),
                new QSpeak("Tišík", "Kde jsi... byl?", ArtName.TisikAngry, SpeakerPosition.Right, Voice.B17_Tisik),
                new QSpeak("Vědátor", "Jsi naživu?", ArtName.VedatorSpeaking, SpeakerPosition.Right, Voice.B18_Ved),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right),
                new QSpeak("Akela", "Jak se vám líbila moje úniková místnost?", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.B18a_Akela),
                new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left),
                new QSpeak("Tišík", "Byla... oukej.", ArtName.TisikThinking, SpeakerPosition.Right, Voice.B19_Tisik),
                new QSpeak("Vědátor", "Strašidelná.", ArtName.VedatorStrach, SpeakerPosition.Right, Voice.B20_Ved),
                new QSpeak("Skok", "Byla v ní všehovšudy jedna hádanka a ani ne moc složitá.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B21_Skok),
                new QSpeak("Skok", "Čekal jsem od tebe víc.", ArtName.SkokThinking, SpeakerPosition.Right, Voice.B22_Skok),
                new QSpeak("Skok", "Možná tuhle hru naprogramoval nějakej lenoch.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B23_Skok),
                new QSetSpeakerArt(ArtName.SkokNormalni, SpeakerPosition.Right),
                new QSpeak("Akela", "Mno, možná je tohle jen technické demo, a plná hra bude mít víc možností.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.B24_Akela),
                new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left),
                new QSpeak("Tišík", "To by bylo dobrý.", ArtName.TisikSpeaking, SpeakerPosition.Right, Voice.B25_Tisik),
                new QSpeak("Skok", "Rozhodně zábavnější.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B26_Skok),
                new QSpeak("Tišík", "Ten příběh o mrtvém majiteli, ..., ten... je pravdivý?", ArtName.TisikThinking, SpeakerPosition.Right, Voice.B27_Tisik),
                new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right),
                new QSpeak("Akela", "To tedy je.", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.B28_Akela),
                new QSpeak("Akela", "A jak moc je skutečný, to se dozvíte možná dřív, než byste chtěli.", ArtName.AkelaThinking, SpeakerPosition.Left, Voice.B29_Akela),
                new QSetBackground(ArtName.Darkness),
                new QWait(0.1f),
                new QSetBackground(ArtName.Thunderbolt),
                new QSfx(SoundEffectName.SfxStormBegins),
                new QWait(0.1f),
                new QSetBackground(ArtName.Darkness),
                new QWait(0.1f),
                new QSetBackground(ArtName.PotemnelaChodba2),
                new QWait(0.2f),
                new QSetBackground(ArtName.PotemnelaChodba3),
                new QSfx(SoundEffectName.SfxMonsterAppears),
                new QWait(0.4f),
                new QSetBackground(ArtName.Darkness),
                new QWait(4f),
                new QSilence(),
                new QWait(1f),
                new QSetSpeakerArt(ArtName.TripleHappy, SpeakerPosition.Right),
                new QSpeak("Profesor", "Děkujeme, že jste si zahráli \"Naší snahou nejlepší buď čin! Najdi cestu ven!\".", ArtName.AkelaExcited, SpeakerPosition.Left),
                new QSpeak("Profesor", "Doufáme, že vás bavila tak moc, jak nás bavilo ji vytvořit.", ArtName.AkelaExplaining, SpeakerPosition.Left),
                new QSpeak("Profesor", "Nechtěli byste náhodou účinkovat v plné verzi této hry?", ArtName.AkelaThinking, SpeakerPosition.Left),
                new QSpeak("Profesor", "Nebo jinak přispět při vývoji?", ArtName.AkelaThinking, SpeakerPosition.Left),
                new QSpeak("Profesor", "Pokud jo, napište nám na naši Facebookovou stránku 'Naší snahou nejlepší buď čin'.", ArtName.AkelaExplaining, SpeakerPosition.Left),
                new QSpeak("Profesor", "Zatím ahoj, a nezapomeňte si mýt ruce ^^.", ArtName.AkelaExcited, SpeakerPosition.Left),
                new QWait(1f),
                new QReturnToMenu()
            });
            yield return new Script(BookmarkId.Door, new QEvent[]
            {
                new QSpeak("", "Dveře jsou zamčené a pevně zavřené. Taky dost staré a zaprášené.", ArtName.Null, SpeakerPosition.Left),
                new QSpeak("Vědátor", "Hej, Tišíku, všiml sis toho nápisu nad klikou?", ArtName.VedatorPointing, SpeakerPosition.Left),
                new QSpeak("Tišík", "Nápisu nad klikou?", ArtName.TisikThinking, SpeakerPosition.Left),
                new QSpeak("Vědátor", "Je tam něco napsáno slabě tužkou.", ArtName.VedatorPointing, SpeakerPosition.Left),
                new QSpeak("Tišík", "Skutečně, vidím tam, \"Anežka, Bětka, Eliška\".", ArtName.TisikPointing, SpeakerPosition.Left),
                new QSpeak("Vědátor", "Možná to jsou dcery původního majitele?", ArtName.VedatorThinking, SpeakerPosition.Left)
            });
                yield return new Script(BookmarkId.Panenka, new QEvent[] {
                    new QSpeak("Tišík", "Tahle panenka se mi vůbec nelíbí. Vypadá hrozivě, jako by mě chtěla zamordovat.", ArtName.TisikStrach, SpeakerPosition.Left),
                    new QSpeak("Skok", "Bojíš se panenek, Tišíku?", ArtName.SkokMluvici, SpeakerPosition.Left),
                    new QSpeak("Tišík", "A ty ne?", ArtName.TisikSpeaking, SpeakerPosition.Left),
                    new QSpeak("Skok", "...trochu.", ArtName.SkokNormalni, SpeakerPosition.Left),
                });
                yield return new Script(BookmarkId.Picture, new QEvent[] {
                    new QSetBackground(ArtName.PictureDetail),
                    new QSpeak("Tišík", "To je rodinná fotka. Takhle velkou rodinu jsem ještě neviděl.", ArtName.TisikSpeaking, SpeakerPosition.Left),
                    new QSpeak("Vědátor", "To je... 2 dospělí, a... um... 6 dětí?", ArtName.VedatorThinking, SpeakerPosition.Left),
                    new QSpeak("Tišík", "Pět holek, a jeden kluk.", ArtName.TisikSpeaking, SpeakerPosition.Left),
                    new QSpeak("Vědátor", "Dokážeš si představit vyrůstat spolu s 5 holkama? To bych nechtěl.", ArtName.VedatorSpeaking, SpeakerPosition.Left),
                    new QSpeak("Tišík", "Já... možná jo?", ArtName.TisikSpeaking, SpeakerPosition.Left),
                    new QSpeak("Vědátor", "Vážně? Proč?", ArtName.VedatorSpeaking, SpeakerPosition.Left),
                    new QSpeak("Tišík", "Jsem jedináček. Má to svoje výhody, ale občas mi chybí mít si s kým hrát. I kdyby to byly holky.", ArtName.TisikPokrcRamen, SpeakerPosition.Left),
                    new QSpeak("Vědátor", "Ah... chápu.", ArtName.VedatorSpeaking, SpeakerPosition.Left),
                    new QSpeak("Vědátor", "Ale podívej na jejich oblečky. Nepřijde ti na nich něco zvláštního?", ArtName.VedatorThinking, SpeakerPosition.Left),
                    new QSpeak("Tišík", "Ta čísla. Každé dítě má na tričku jiné.", ArtName.TisikThinking, SpeakerPosition.Left),
                    new QSpeak("Vědátor", "Co to může znamenat?", ArtName.VedatorThinking, SpeakerPosition.Left),
                    new QPopScene(),
                });
                yield return new Script(BookmarkId.TrezorNapoveda, new QEvent[] {
                    new QSpeak("Tišík", "Tři čísla. Kde jen jsem v této místnosti viděl čísla?", ArtName.TisikSpeaking, SpeakerPosition.Left),
                    new QSpeak("Vědátor", "Na té fotce. Ale která z nich máme použít? A v jakém pořadí?", ArtName.VedatorSpeaking, SpeakerPosition.Left),
                    new QSpeak("Tišík", "Hmm...", ArtName.TisikThinking, SpeakerPosition.Left),
                    new QSpeak("Vědátor", "Možná bychom se měli znovu podívat na dveře ven?", ArtName.VedatorThinking, SpeakerPosition.Left),
                });
                yield return new Script(BookmarkId.Picture2, new QEvent[] {
                    new QSetBackground(ArtName.PictureDetail),
                    new QSpeak("", "Rodinná fotka. Každé dítě má na svém oblečení jiné číslo.", ArtName.Null, SpeakerPosition.Left),
                    new QSetBackground(ArtName.InteriorDrizzleRain),
                    new QPopScene(), 
                });
                yield return new Script(BookmarkId.Trezor, new QEvent[] {
                    new QSpeak("Tišík", "Wow. To je bytelný kovový trezor.", ArtName.TisikSpeaking, SpeakerPosition.Left),
                    new QSpeak("Skok", "Vypadá vojensky. Neproniknutelně.", ArtName.SkokThinking, SpeakerPosition.Left),
                    new QSpeak("Tišík", "Vojensky? Myslíš, že jsou uvnitř zbraně?", ArtName.TisikStrach, SpeakerPosition.Left),
                    new QSpeak("Skok", "Těžko říct. Mohly by.", ArtName.SkokMluvici, SpeakerPosition.Left),
                    new QSpeak("Vědátor", "Na zbraně bychom neměli šahat.", ArtName.VedatorStrach, SpeakerPosition.Left),
                    new QSpeak("Tišík", "To vím taky. Ale co když je tam něco jiného, co by se nám hodilo?", ArtName.TisikAngry, SpeakerPosition.Left),
                    new QSpeak("Skok", "Zdá se, že ten trezor chce tříčíselný kód.", ArtName.SkokMluvici, SpeakerPosition.Left),
                    new QSpeak("Tišík", "Tříčíselný kód? Něco mi říká, že ho najdeme v této místnosti.", ArtName.TisikSpeaking, SpeakerPosition.Left, auxiliaryAction: new QSpeak.AuxiliaryAction(G.T("Zadat kód"),
                        (session) =>
                        {
                            session.EnterPuzzle(new TrezorPuzzle());
                        }
                    ))
            });
                yield return new Script(BookmarkId.Trezor2, new QEvent[] {
                    new QSpeak("", "Kovový trezor, zamčený na tříčíselný kód.", ArtName.Null, SpeakerPosition.Left, auxiliaryAction: new QSpeak.AuxiliaryAction(G.T("Zadat kód"),
                        session =>
                        {
                            session.EnterPuzzle(new TrezorPuzzle());
                        }
                    ))
            });
                yield return new Script(BookmarkId.Window, new QEvent[] {
                    new QSpeak("Tišík", "Venku je pořád příšerná bouřka.", ArtName.TisikSpeaking, SpeakerPosition.Left),
                    new QSpeak("Vědátor", "Myslíš, že se časem přežene?", ArtName.VedatorSpeaking, SpeakerPosition.Left),
                    new QSpeak("Tišík", "Huh? Myslíš, že by nemusela?", ArtName.TisikThinking, SpeakerPosition.Left),
                    new QSpeak("Vědátor", "Slyšel jsem, že v tropech deště někdy trvají celé měsíce?", ArtName.VedatorThinking, SpeakerPosition.Left),
                    new QSpeak("Skok", "Kluci, jsem si jistý, že ta bouřka brzy přejde.", ArtName.SkokMluvici, SpeakerPosition.Left),
            });
              
        }
    }
}