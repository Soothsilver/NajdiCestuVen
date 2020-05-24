using System.Collections.Generic;
using Auxiliary;
using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;

namespace Nsnbc.Android.Stories
{
    public class FullStory
    {
        public static IEnumerable<QEvent> EnqueueStory(StoryID id)
        {
            switch (id)
            {
                case StoryID.Intro:
                    yield return new QSetBackground(ArtName.Exterior);
                    yield return new QZoomInto(new Rectangle(115, 226, 600, 337), 12);
                    yield return new QWait(2);
                    yield return new QSpeak("Akela", "Vlčata! Vítejte na vícedenní výpravě v Chatě teroru!", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QSpeak("Vlčata", "Jupí!!!", ArtName.TriVlcata, SpeakerPosition.Right);
                    yield return new QSpeak("Vědátor", "Počkat... chatě teroru?", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSetBackground(ArtName.InteriorGood);
                    yield return new QSetScene(new FirstScene());
                    yield return new QSpeak("Akela", "Ano, přesně tak. Majitel této chaty zemřel před sedmi lety.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSpeak("Akela", "A to přímo v této místnosti, kde jeho tělo po sedmi dnech našel soused.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Co... co se mu stalo?", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "To nikdo neví, ale místní říkají, že za života to byl zlý a nenávistný člověk.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "A že jeho duch stále bloudí po tomto pozemku.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSpeak("Akela", "A hledá nové způsoby, jak se pomstít všem, kdo ho přežili.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSpeak("", "...", ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QSpeak("Akela", "...mno, a proto byl účastnický poplatek za tuto výpravu tak malý.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSetBackground(ArtName.InteriorDrizzleRain);
                    yield return new QSpeak("Akela", "Každopádně, můžete si tady teď rozbalit věci, já půjdu ven připravit hru, a za chvilku se pro vás vrátím.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSpeak("Akela", "Do té doby vás má na starosti Skok.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Um... Akelo? Začíná pršet.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "Hmm... skutečně... mno, jak říká skautský zákon, skaut je nepromokavý. Připravte se tedy i na hru v dešti!", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSpeak("Akela", "Tak jo, Skoku, máš vedení. Mějte se, jsem zpátky za sedm minut, rozbijte toho co nejmíň. Akela out.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Right);
                    yield return new QSetSpeakerArt(ArtName.Null,SpeakerPosition.Right);  
                    yield return new QWait(1);
                    yield return new QSetScene(null);
                    yield return new QSetBackground(ArtName.Darkness);           
                    yield return new QWait(0.1f); 
                    yield return new QSetBackground(ArtName.Thunderbolt);          
                    yield return new QWait(0.2f); 
                    yield return new QSetBackground(ArtName.Darkness);  
                    yield return new QSpeak("Vědátor", "Aah!", ArtName.Null, SpeakerPosition.Left);        
                    yield return new QWait(2f); 
                    yield return new QSpeak("", "O třicet minut později.", ArtName.Null, SpeakerPosition.Left);
                    yield return new QSetBackground(ArtName.InteriorDrizzleRain);
                    yield return new QSetScene(new FirstScene());
                    yield return new QSpeak("Vědátor", "Deštík, Tišíku, jo? Tohle je blesková bouře, jakou jsem ještě neviděl.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "V tomhle přece nemůže chtít, abychom šli ven. Je to nebezpečný!", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Je to nebezpečný i pro něj. Už měl dávno být zpátky.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Akela?", ArtName.TisikExplanation, SpeakerPosition.Left);        
                    yield return new QSpeak("Skok", "Jo. Půjdu se podívat na zápraží.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("", "Skok se pokusí odemknout dveře, ale jsou zamčené.", ArtName.Null, SpeakerPosition.Left);    
                    yield return new QSpeak("Skok", "On... nás tady zamknul?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Říkal, že máme zůstat v této místnosti.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Ale zamknout dveře?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Myslíte, že se mu něco stalo?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Třeba do něj uhodil blesk...?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Nebo... možná ten duch...?", ArtName.TisikExplanation, SpeakerPosition.Left);     
                    yield return new QWait(1f); 
                    yield return new QSpeak("Tišík", "Musíme Akelu najít. Co když se mu fakt něco stalo?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Souhlasím. A jako první tedy musíme otevřít tyto dveře.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Prohledejme místnost. Někde tady musí být náhradní klíč.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QEndSpeaking();
                    yield return new QFlyFromCenter(ArtName.Najdi, 1);
                    yield return new QFlyFromCenter(ArtName.Cestu, 1);
                    yield return new QFlyFromCenter(ArtName.Ven, 1);
                    yield return new QWait(1, true);
                    yield return new QEndFlyouts();
                    yield return new QAction((session) => session.YouHaveControl = true);
                    break;
                case StoryID.Victory:
                    yield return new QFlyFromCenter(ArtName.YouFoundIt, 1);
                    yield return new QWait(2, true);
                    yield return new QEndFlyouts();
                    yield return new QAction((s) =>
                    {
                        s.Scene = null;
                        s.Inventory.Clear();
                        s.CurrentZoom = s.FullResolution;
                    });
                    yield return new QSetBackground(ArtName.PotemnelaChodba1);
                    yield return new QSetSpeakerArt(ArtName.Null, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "A-Akelo?", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Vědátor", "Možná je na záchodě?", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Skok", "To těžko. Akelovi to nikdy netrvá víc než sedm minut.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Vědátor", "Takže... stále venku?", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Skok", "Může být.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("", "Připravili jsme se s k otevření hlavních venkovních dveří.", ArtName.Null, SpeakerPosition.Right);
                    yield return new QSpeak("Tišík", "Na tři?", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Vědátor", "Na tři.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Skok", "Raz --", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Skok", "Dva --", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Skok", "Tři.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSetBackground(ArtName.PotemnelaChodba2);
                    yield return new QSpeak("", "Skok vezme za kliku a dveře se otevřou. Venku lije hromový déšť--", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "Baf!", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSetSpeakerArt(ArtName.AkelaNormal, SpeakerPosition.Left);
                    yield return new QSpeak("Vlčata", "Aah!", ArtName.TriVlcata, SpeakerPosition.Right);
                    yield return new QSpeak("Tišík", "Akelo!", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Vědátor", "Báli jsme se!", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Tišík", "Kde jsi byl?", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Vědátor", "Jsi naživu?", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "Jak se vám líbila moje úniková místnost?", ArtName.TriVlcata, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Byla... oukej.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Vědátor", "Strašidelná.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Skok", "Byla v ní všehovšudy jedna hádanka a to ani ne moc složitá.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Skok", "Čekal jsem od tebe víc.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Skok", "Možná tuhle hru naprogramoval nějakej lenoch.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "(odkašle si do rukávu) Mno, možná je tohle jen technické demo, a plná hra bude mít více místností.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "To by bylo dobrý.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Skok", "Rozhodně zábavnější.", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Tišík", "Ten příběh o mrtvém majiteli, ..., ten je skutečný?", ArtName.TisikExplanation, SpeakerPosition.Right);
                    yield return new QSpeak("Akela", "To tedy je.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSpeak("Akela", "A jak moc je skutečný, to se dozvíte možná dřív, než byste chtěli.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSetBackground(ArtName.Darkness);
                    yield return new QWait(0.1f);
                    yield return new QSetBackground(ArtName.Thunderbolt);
                    yield return new QWait(0.1f);
                    yield return new QSetBackground(ArtName.Darkness);
                    yield return new QWait(0.1f);
                    yield return new QSetBackground(ArtName.PotemnelaChodba2);
                    yield return new QWait(0.2f);
                    yield return new QSetBackground(ArtName.PotemnelaChodba3);
                    yield return new QWait(5.5f);
                    yield return new QSetBackground(ArtName.Darkness);
                    yield return new QWait(2f);
                    yield return new QSpeak("Programátor", "Děkujeme, že jste si zahráli \"Naší snahou nejlepší buď čin! Najdi cestu ven!\".", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSpeak("Programátor", "Doufáme, že vás bavila tak moc, jak nás bavilo ji vytvořit.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSpeak("Programátor", "Pokud byste chtěli přispět do vývoje plné verze nebo pokud byste chtěli propůjčit svůj hlas postavě v plné verzi, určitě napište, např. na facebookový kanál Naší snahou nejlepší buď čin.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QSpeak("Programátor", "Zatím ahoj, a nezapomeňte si mýt ruce ^^.", ArtName.AkelaExcited, SpeakerPosition.Left);
                    yield return new QAction((s) =>
                    {
                        s.IncomingEvents.Clear();
                        Root.PopFromPhase();
                    });
                    break;
                case StoryID.Door:
                    yield return new QSpeak("", "Dveře jsou zamčené a pevně zavřené. Taky dost staré a zaprášené.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Hej, Tišíku, všiml sis toho nápis nad klikou?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Nápisu nad klikou?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Je tam něco napsáno slabě tužkou.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Skutečně, vidím tam, \"Anežka, Bětka, Eliška\".", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Možná to jsou dcery původního majitele?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    break;
                case StoryID.Panenka:
                    yield return new QSpeak("Tišík", "Tahle panenka se mi vůbec nelíbí. Vypadá hrozivě, jako by mě chtěla zamordovat.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Bojíš se panenek, Tišíku?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "A ty ne?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "...trochu.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    break;
                case StoryID.Picture:
                    yield return new QPushZoomAndScene();
                    yield return new QSetBackground(ArtName.PictureDetail);
                    yield return new QSpeak("Tišík", "To je rodinná fotka. Takhle velkou rodinu jsem ještě neviděl.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "To je... 2 dospělí, a... um... 6 dětí?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Pět holek, a jeden kluk.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Dokážeš si představit vyrůstat spolu s 5 holkama? To bych nechtěl.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Já... možná jo?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Vážně? Proč?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Jsem jedináček. Má to svoje výhody, ale občas mi chybí mít si s kým hrát. I kdyby to byly holky.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Ah... chápu.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Ale podívej na jejich oblečky. Nepřijde ti na nich něco zvláštního?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Ta čísla. Každý má na triku jiné.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Co to může znamenat?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSetBackground(ArtName.InteriorDrizzleRain);
                    yield return new QPopZoomAndScene();
                    break;
                case StoryID.TrezorNapoveda:
                    yield return new QSpeak("Tišík", "Tři čísla. Kde jen jsem v této místnosti viděl čísla?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Na té fotce. Ale která z nich máme použít? A v jakém pořadí?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Hmm...", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Možná bychom se měli znovu podívat na dveře ven?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    break;
                case StoryID.Picture2:
                    yield return new QPushZoomAndScene();
                    yield return new QSetBackground(ArtName.PictureDetail);
                    yield return new QSpeak("", "Rodinná fotka. Každé dítě má na svém oblečení jiné číslo.", ArtName.Null, SpeakerPosition.Left);
                    yield return new QSetBackground(ArtName.InteriorDrizzleRain);
                    yield return new QPopZoomAndScene();
                    break;
                case StoryID.Trezor:
                    yield return new QSpeak("Tišík", "Wow. To je bytelný kovový trezor.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Vypadá vojensky. Neproniknutelně.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Vojensky? Myslíš, že jsou uvnitř zbraně?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Těžko říct. Mohly by.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Na zbraně bychom neměli šahat.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "To vím taky. Ale co když je tam něco jiného, co by se nám hodilo?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Zdá se, že ten trezor chce tříčíselný kód.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Tříčíselný kód? Něco mi říká, že ho najdeme v této místnosti.", ArtName.TisikExplanation, SpeakerPosition.Left)
                    {
                        AuxiActionName = "Zadat kód",
                        AuxiAction = (session) =>
                        {
                            session.EnterPuzzle(new TrezorPuzzle());
                        }
                    };
                    break;
                case StoryID.Trezor2:
                    yield return new QSpeak("", "Kovový trezor, zamčený na tříčíselný kód.", ArtName.Null, SpeakerPosition.Left)
                    {
                        AuxiActionName = "Zadat kód",
                        AuxiAction = (session) =>
                        {
                            session.EnterPuzzle(new TrezorPuzzle());
                        }
                    };
                    break;
                case StoryID.Window:
                    yield return new QSpeak("Tišík", "Venku je pořád příšerná bouřka.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Myslíš, že se časem přežene?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Tišík", "Huh? Myslíš, že by nemusela?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Vědátor", "Slyšel jsem, že v tropech deště někdy trvají celé měsíce?", ArtName.TisikExplanation, SpeakerPosition.Left);
                    yield return new QSpeak("Skok", "Kluci, jsem si jistý, že ta bouřka brzy přejde.", ArtName.TisikExplanation, SpeakerPosition.Left);
                    break;
            }
        }
    }

    public class QEndFlyouts : QEvent
    {
        public override void Begin(TSession session)
        {
            session.ActiveActities.RemoveAll(act => act is QFlyFromCenter);
        }
    }

    public class QFlyFromCenter : QEvent, IDrawableActivity
    {
        private readonly ArtName art;
        private readonly float seconds;
        private float percentage;
        private float percentageSpeed;

        public QFlyFromCenter(ArtName art, float seconds)
        {
            this.art = art;
            this.seconds = seconds;
            percentageSpeed = 1f / seconds;
        }

        public override void Begin(TSession session)
        {
            session.ActiveActities.Add(this);
            session.QuickEnqueue(new QWait(seconds, true));
        }

        public bool Blocking => false;
        public bool Dead { get; set; }
        public void Run(TSession session, float elapsedSeconds)
        {
            percentage += percentageSpeed * elapsedSeconds;
            if (percentage >= 1)
            {
                percentage = 1;
            }
        }

        public void Draw()
        {
            int width = (int) (Root.Screen.Width * percentage);
            int height = (int) (Root.Screen.Height * percentage);
            Primitives.DrawImage(Library.Art(art), new Rectangle(
                Root.Screen.Width/2-width/2,
                Root.Screen.Height/2-height/2,
                width,
                height
                ));
        }
    }
}