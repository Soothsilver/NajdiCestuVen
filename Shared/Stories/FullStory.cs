using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;

namespace Nsnbc.Android.Stories
{
    public class FullStory
    {
        public static IEnumerable<QEvent> EnqueueStory(StoryId id)
        {
            switch (id)
            {
                case StoryId.Intro:
                    yield return new QSetBackground(ArtName.Exterior);
                    yield return new QAction((s)=>Sfxs.BeginSong(Sfxs.MusicStory));
                    yield return new QZoomInto(new Rectangle(115, 226, 600, 337), 12);
                    yield return new QWait(2);
                    yield return new QSpeak("Akela", "Vlčata! Vítejte na vícedenní výpravě v Chatě teroru!", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.A1_Akela);
                    yield return new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QSpeak("Vlčata", "Jupí!!!", ArtName.TripleHappy, SpeakerPosition.Right, Voice.A2_TriJoin);
                    yield return new QSpeak("Vědátor", "Počkat... chatě teroru?", ArtName.VedatorStrach, SpeakerPosition.Right, Voice.A3_Ved);
                    yield return new QSetBackground(ArtName.InteriorGood);
                    yield return new QSetScene(new FirstScene());
                    yield return new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "Ano, přesně tak. Majitel této chaty zemřel před sedmi lety.", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.A4_Akela);
                    yield return new QSpeak("Akela", "A to přímo v této místnosti, kde jeho tělo po sedmi dnech našel soused.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A5_Akela);
                    yield return new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Co se mu s-stalo?", ArtName.TisikStrach, SpeakerPosition.Right, Voice.A6_Tisik);
                    yield return new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "To nikdo neví, ale místní říkají, že za života to byl zlý a nenávistný člověk.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A7_Akela);
                    yield return new QSpeak("Akela", "A že jeho duch stále bloudí po tomto pozemku.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A8_Akela);
                    yield return new QSpeak("Akela", "A hledá nové způsoby, jak se pomstít všem, kdo ho přežili.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A9_Akela);
                    yield return new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QWait(1);
                    yield return new QSpeak("Akela", "...jo, a proto byl účastnický poplatek za tuto výpravu tak malý.", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.A10_Akela);
                    yield return new QSetBackground(ArtName.InteriorDrizzleRain);
                    yield return new QAction((s) => Sfxs.BeginRain(0.05f));
                    yield return new QSpeak("Akela", "Každopádně, můžete si tady teď rozbalit věci, já půjdu ven připravit hru, a za chvilku se pro vás vrátím.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A11_Akela);
                    yield return new QSpeak("Akela", "Do té doby vás má na starosti Skok.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.A12_Akela);
                    yield return new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Um... Akelo? Začíná pršet.", ArtName.TisikPointing, SpeakerPosition.Right, Voice.A13_Tisik);
                    yield return new QSetSpeakerArt(ArtName.Null,SpeakerPosition.Right);  
                    yield return new QSpeak("Akela", "Hmm... skutečně? Mno, jak říká skautský zákon, skaut je nepromokavý! Připravte se na hru v dešti!", ArtName.AkelaThinking, SpeakerPosition.Left, Voice.A14_Akela);
                    yield return new QSpeak("Akela", "Tak jo, Skoku, máš vedení. Mějte se, jsem zpátky za sedm minut, rozbijte toho co nejmíň. Akela out.", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.A16_Akela);
                    yield return new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right);
                    yield return new QSetSpeakerArt(ArtName.Null,SpeakerPosition.Right);  
                    yield return new QWait(1);
                    yield return new QSetScene(null);
                    yield return new QSetBackground(ArtName.Darkness);           
                    yield return new QWait(0.1f); 
                    yield return new QSetBackground(ArtName.Thunderbolt);
                    yield return new QSfx(Sfxs.SfxThunder);        
                    yield return new QWait(0.2f); 
                    yield return new QSetBackground(ArtName.Darkness);  
                    yield return new QWait(2f); 
                    yield return new QSpeak("", "O třicet minut později.", ArtName.Null, SpeakerPosition.Left, Voice.A17a_Tisik);
                    yield return new QSetBackground(ArtName.InteriorDrizzleRain);
                    yield return new QSetScene(new FirstScene());
                    yield return new QAction((s) => Sfxs.BeginRain(0.1f));
                    yield return new QSfx(Sfxs.SfxStormBegins, 0.25f);        
                    yield return new QWait(2f); 
                    yield return new QSpeak("Vědátor", "Deštík, Tišíku, jo? Tohle je blesková bouře, jakou jsem ještě neviděl.", ArtName.VedatorSpeaking, SpeakerPosition.Left, Voice.A18_Ved);
                    yield return new QSpeak("Tišík", "V tomhle přece nemůže chtít, abychom šli ven!", ArtName.TisikAngry, SpeakerPosition.Left, Voice.A19_Tisik);
                    yield return new QSpeak("Skok", "Je to nebezpečný i pro něj. Už měl dávno být zpátky.", ArtName.SkokNastvany, SpeakerPosition.Left, Voice.A20_Skok);
                    yield return new QSpeak("Tišík", "Akela?", ArtName.TisikThinking, SpeakerPosition.Left, Voice.A21_Tisik);        
                    yield return new QSpeak("Skok", "Jo. Půjdu se podívat na zápraží.", ArtName.SkokMluvici, SpeakerPosition.Left, Voice.A22_Skok);
                    yield return new QSfx(Sfxs.SfxDoorHandle);        
                    yield return new QSpeak("", "Skok se pokusí odemknout dveře, ale jsou zamčené.", ArtName.Null, SpeakerPosition.Left, Voice.Silence);    
                    yield return new QSpeak("Skok", "On... nás tady zamknul?", ArtName.SkokNastvany, SpeakerPosition.Left, Voice.A23_Skok);
                    yield return new QSpeak("Tišík", "Říkal, že máme zůstat v téhle místnosti.", ArtName.TisikSpeaking, SpeakerPosition.Left, Voice.A24_Ved);
                    yield return new QSpeak("Skok", "Ale zamknout dveře?", ArtName.SkokThinking, SpeakerPosition.Left, Voice.A25_Skok);
                    yield return new QSpeak("Tišík", "Myslíte, že se mu něco stalo?", ArtName.TisikThinking, SpeakerPosition.Left, Voice.A26_Tisik);
                    yield return new QSpeak("Skok", "Třeba do něj uhodil blesk...?", ArtName.SkokThinking, SpeakerPosition.Left, Voice.A27_Skok);
                    yield return new QSpeak("Vědátor", "Nebo... možná ten duch...?", ArtName.VedatorStrach, SpeakerPosition.Left, Voice.A28_Ved);     
                    yield return new QWait(1f); 
                    yield return new QSpeak("Tišík", "Musíme Akelu najít. Co když se mu fakt něco stalo?", ArtName.TisikDetermined, SpeakerPosition.Left, Voice.A29_Tisik);
                    yield return new QSpeak("Skok", "Souhlasím. A jako první tedy musíme otevřít tyto dveře.", ArtName.SkokMluvici, SpeakerPosition.Left, Voice.A30_Skok);
                    yield return new QSpeak("Skok", "Prohledejme místnost. Někde tady musí být náhradní klíč.", ArtName.SkokMluvici, SpeakerPosition.Left, Voice.A31_Skok);
                    yield return new QEndSpeaking();
                    yield return new QAction((s) => Sfxs.Silence());
                    yield return new QSfx(Sfxs.SfxWhoosh);
                    yield return new QFlyFromCenter(ArtName.Najdi, 1);
                    yield return new QSfx(Sfxs.SfxWhoosh);
                    yield return new QFlyFromCenter(ArtName.Cestu, 1);
                    yield return new QSfx(Sfxs.SfxWhoosh);
                    yield return new QFlyFromCenter(ArtName.Ven, 1);
                    yield return new QWait(1, true);
                    yield return new QEndFlyouts();
                    yield return new QAction((s)=>Eqatec.Send("SEEK A WAY OUT"));
                    yield return new QAction((session) => session.YouHaveControl = true);
                    yield return new QAction((s)=>
                    {
                        Sfxs.BeginSong(Sfxs.MusicGameplay);
                        Sfxs.BeginRain(0.05f);
                    });
                    break;
                case StoryId.Victory:
                    yield return new QAction((s) => Sfxs.Silence());
                    yield return new QSfx(Sfxs.SfxSuccess);
                    yield return new QFlyFromCenter(ArtName.YouFoundIt, 1);
                    yield return new QWait(2, true);
                    yield return new QAction((s)=>Eqatec.Send("YOU FOUND IT"));
                    yield return new QEndFlyouts();
                    yield return new QAction((s) =>
                    {
                        s.Scene = null;
                        s.Inventory.Clear();
                        s.CurrentZoom = s.FullResolution;
                        Sfxs.BeginSong(Sfxs.MusicStory);
                        Sfxs.BeginRain(0.05f);
                    });
                    yield return new QSetBackground(ArtName.PotemnelaChodba1);
                    yield return new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "...Akelo?", ArtName.TisikSpeaking, SpeakerPosition.Right, Voice.B1_Tisik);
                    yield return new QSpeak("Vědátor", "Možná je na záchodě?", ArtName.VedatorThinking, SpeakerPosition.Right, Voice.B2_Ved);
                    yield return new QSpeak("Skok", "To těžko. Akelovi to nikdy netrvá víc než sedm minut.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B3_Skok);
                    yield return new QSpeak("Vědátor", "Takže... stále venku?", ArtName.VedatorThinking, SpeakerPosition.Right, Voice.B4_Ved);
                    yield return new QSpeak("Skok", "Může být.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B5_Skok);
                    yield return new QSpeak("", "Připravili jsme se k otevření hlavních venkovních dveří.", ArtName.Null, SpeakerPosition.Right, Voice.B6_Tisik);
                    yield return new QSpeak("Tišík", "Na tři?", ArtName.TisikDetermined, SpeakerPosition.Right, Voice.B7_Tisik);
                    yield return new QSpeak("Vědátor", "Na tři.", ArtName.VedatorSpeaking, SpeakerPosition.Right, Voice.B8_Ved);
                    yield return new QSpeak("Skok", "Raz --", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B9_Skok);
                    yield return new QSpeak("Skok", "Dva --", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B10_Skok);
                    yield return new QSpeak("Skok", "Tři.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B11_Skok);
                    yield return new QSetBackground(ArtName.PotemnelaChodba2);
                    yield return new QSfx(Sfxs.SfxDoorHandle);
                    yield return new QAction((s) => Sfxs.BeginRain(0.075f));
                    yield return new QSpeak("", "Skok vezme za kliku a dveře se otevřou. Venku lije hromový déšť--", ArtName.Null, SpeakerPosition.Right, Voice.B12_Tisik);
                    yield return new QSpeak("Akela", "Baf!", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.B13_Akela);
                    yield return new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QSpeak("Vlčata", "Aah!", ArtName.TripleFear, SpeakerPosition.Right, Voice.B14_Tri);
                    yield return new QSpeak("Tišík", "Akelo!", ArtName.TisikAngry, SpeakerPosition.Right, Voice.B15_Tisik);
                    yield return new QSpeak("Vědátor", "Báli jsme se!", ArtName.VedatorStrach, SpeakerPosition.Right, Voice.B16_Ved);
                    yield return new QSpeak("Tišík", "Kde jsi... byl?", ArtName.TisikAngry, SpeakerPosition.Right, Voice.B17_Tisik);
                    yield return new QSpeak("Vědátor", "Jsi naživu?", ArtName.VedatorSpeaking, SpeakerPosition.Right, Voice.B18_Ved);
                    yield return new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "Jak se vám líbila moje úniková místnost?", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.B18a_Akela);       
                    yield return new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Byla... oukej.", ArtName.TisikThinking, SpeakerPosition.Right, Voice.B19_Tisik);
                    yield return new QSpeak("Vědátor", "Strašidelná.", ArtName.VedatorStrach, SpeakerPosition.Right, Voice.B20_Ved);
                    yield return new QSpeak("Skok", "Byla v ní všehovšudy jedna hádanka a ani ne moc složitá.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B21_Skok);
                    yield return new QSpeak("Skok", "Čekal jsem od tebe víc.", ArtName.SkokThinking, SpeakerPosition.Right, Voice.B22_Skok);
                    yield return new QSpeak("Skok", "Možná tuhle hru naprogramoval nějakej lenoch.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B23_Skok);
                    yield return new QSetSpeakerArt(ArtName.SkokNormalni, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "Mno, možná je tohle jen technické demo, a plná hra bude mít víc možností.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.B24_Akela);
                    yield return new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "To by bylo dobrý.", ArtName.TisikSpeaking, SpeakerPosition.Right, Voice.B25_Tisik);
                    yield return new QSpeak("Skok", "Rozhodně zábavnější.", ArtName.SkokMluvici, SpeakerPosition.Right, Voice.B26_Skok);
                    yield return new QSpeak("Tišík", "Ten příběh o mrtvém majiteli, ..., ten... je pravdivý?", ArtName.TisikThinking, SpeakerPosition.Right, Voice.B27_Tisik);
                    yield return new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "To tedy je.", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.B28_Akela);
                    yield return new QSpeak("Akela", "A jak moc je skutečný, to se dozvíte možná dřív, než byste chtěli.", ArtName.AkelaThinking, SpeakerPosition.Left, Voice.B29_Akela);
                    yield return new QSetBackground(ArtName.Darkness);
                    yield return new QWait(0.1f);
                    yield return new QSetBackground(ArtName.Thunderbolt);
                    yield return new QSfx(Sfxs.SfxStormBegins);
                    yield return new QWait(0.1f);
                    yield return new QSetBackground(ArtName.Darkness);
                    yield return new QWait(0.1f);
                    yield return new QSetBackground(ArtName.PotemnelaChodba2);
                    yield return new QWait(0.2f);
                    yield return new QSetBackground(ArtName.PotemnelaChodba3);
                    yield return new QSfx(Sfxs.SfxMonsterAppears);
                    yield return new QWait(0.4f);
                    yield return new QSetBackground(ArtName.Darkness);
                    yield return new QWait(4f);
                    yield return new QAction((s) => Sfxs.Silence());
                    yield return new QWait(1f);                   
                    yield return new QSetSpeakerArt(ArtName.TripleHappy, SpeakerPosition.Right);
                    yield return new QSpeak("Profesor", "Děkujeme, že jste si zahráli \"Naší snahou nejlepší buď čin! Najdi cestu ven!\".", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.Null);
                    yield return new QSpeak("Profesor", "Doufáme, že vás bavila tak moc, jak nás bavilo ji vytvořit.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.Null);
                    yield return new QSpeak("Profesor", "Nechtěli byste náhodou účinkovat v plné verzi této hry?", ArtName.AkelaThinking, SpeakerPosition.Left, Voice.Null);
                    yield return new QSpeak("Profesor", "Nebo jinak přispět při vývoji?", ArtName.AkelaThinking, SpeakerPosition.Left, Voice.Null);
                    yield return new QSpeak("Profesor", "Pokud jo, napište nám na naši Facebookovou stránku 'Naší snahou nejlepší buď čin'.", ArtName.AkelaExplaining, SpeakerPosition.Left, Voice.Null);
                    yield return new QSpeak("Profesor", "Zatím ahoj, a nezapomeňte si mýt ruce ^^.", ArtName.AkelaExcited, SpeakerPosition.Left, Voice.Null);
                    yield return new QWait(1f);                   
                    yield return new QAction((s) =>
                    {
                        s.IncomingEvents.Clear();
                        Root.PopFromPhase();
                    });
                    break;
                case StoryId.Door:
                    yield return new QSpeak("", "Dveře jsou zamčené a pevně zavřené. Taky dost staré a zaprášené.", ArtName.Null, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Hej, Tišíku, všiml sis toho nápisu nad klikou?", ArtName.VedatorPointing, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Nápisu nad klikou?", ArtName.TisikThinking, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Je tam něco napsáno slabě tužkou.", ArtName.VedatorPointing, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Skutečně, vidím tam, \"Anežka, Bětka, Eliška\".", ArtName.TisikPointing, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Možná to jsou dcery původního majitele?", ArtName.VedatorThinking, SpeakerPosition.Left);
                    break;
                case StoryId.Panenka:
                    yield return new QSpeak("Tišík", "Tahle panenka se mi vůbec nelíbí. Vypadá hrozivě, jako by mě chtěla zamordovat.", ArtName.TisikStrach, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Bojíš se panenek, Tišíku?", ArtName.SkokMluvici, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "A ty ne?", ArtName.TisikSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "...trochu.", ArtName.SkokNormalni, SpeakerPosition.Left);
                    break;
                case StoryId.Picture:
                    yield return new QPushZoomAndScene();
                    yield return new QSetBackground(ArtName.PictureDetail);
                    yield return new QSpeak("Tišík", "To je rodinná fotka. Takhle velkou rodinu jsem ještě neviděl.", ArtName.TisikSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "To je... 2 dospělí, a... um... 6 dětí?", ArtName.VedatorThinking, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Pět holek, a jeden kluk.", ArtName.TisikSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Dokážeš si představit vyrůstat spolu s 5 holkama? To bych nechtěl.", ArtName.VedatorSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Já... možná jo?", ArtName.TisikSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Vážně? Proč?", ArtName.VedatorSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Jsem jedináček. Má to svoje výhody, ale občas mi chybí mít si s kým hrát. I kdyby to byly holky.", ArtName.TisikPokrcRamen, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Ah... chápu.", ArtName.VedatorSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Ale podívej na jejich oblečky. Nepřijde ti na nich něco zvláštního?", ArtName.VedatorThinking, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Ta čísla. Každé dítě má na tričku jiné.", ArtName.TisikThinking, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Co to může znamenat?", ArtName.VedatorThinking, SpeakerPosition.Left);
                    yield return new QSetBackground(ArtName.InteriorDrizzleRain);
                    yield return new QPopZoomAndScene();
                    break;
                case StoryId.TrezorNapoveda:
                    yield return new QSpeak("Tišík", "Tři čísla. Kde jen jsem v této místnosti viděl čísla?", ArtName.TisikSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Na té fotce. Ale která z nich máme použít? A v jakém pořadí?", ArtName.VedatorSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Hmm...", ArtName.TisikThinking, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Možná bychom se měli znovu podívat na dveře ven?", ArtName.VedatorThinking, SpeakerPosition.Left);
                    break;
                case StoryId.Picture2:
                    yield return new QPushZoomAndScene();
                    yield return new QSetBackground(ArtName.PictureDetail);
                    yield return new QSpeak("", "Rodinná fotka. Každé dítě má na svém oblečení jiné číslo.", ArtName.Null, SpeakerPosition.Left);
                    yield return new QSetBackground(ArtName.InteriorDrizzleRain);
                    yield return new QPopZoomAndScene();
                    break;
                case StoryId.Trezor:
                    yield return new QSpeak("Tišík", "Wow. To je bytelný kovový trezor.", ArtName.TisikSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Vypadá vojensky. Neproniknutelně.", ArtName.SkokThinking, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Vojensky? Myslíš, že jsou uvnitř zbraně?", ArtName.TisikStrach, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Těžko říct. Mohly by.", ArtName.SkokMluvici, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Na zbraně bychom neměli šahat.", ArtName.VedatorStrach, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "To vím taky. Ale co když je tam něco jiného, co by se nám hodilo?", ArtName.TisikAngry, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Zdá se, že ten trezor chce tříčíselný kód.", ArtName.SkokMluvici, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Tříčíselný kód? Něco mi říká, že ho najdeme v této místnosti.", ArtName.TisikSpeaking, SpeakerPosition.Left)
                    {
                        AuxiActionName = "Zadat kód",
                        AuxiAction = (session) =>
                        {
                            session.EnterPuzzle(new TrezorPuzzle());
                        }
                    };
                    break;
                case StoryId.Trezor2:
                    yield return new QSpeak("", "Kovový trezor, zamčený na tříčíselný kód.", ArtName.Null, SpeakerPosition.Left)
                    {
                        AuxiActionName = "Zadat kód",
                        AuxiAction = (session) =>
                        {
                            session.EnterPuzzle(new TrezorPuzzle());
                        }
                    };
                    break;
                case StoryId.Window:
                    yield return new QSpeak("Tišík", "Venku je pořád příšerná bouřka.", ArtName.TisikSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Myslíš, že se časem přežene?", ArtName.VedatorSpeaking, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Huh? Myslíš, že by nemusela?", ArtName.TisikThinking, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Slyšel jsem, že v tropech deště někdy trvají celé měsíce?", ArtName.VedatorThinking, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Kluci, jsem si jistý, že ta bouřka brzy přejde.", ArtName.SkokMluvici, SpeakerPosition.Left);
                    break;
            }
        }
    }
}