using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.PostSharp;
using Nsnbc.SerializableCode;
using Nsnbc.Sounds;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Scenes
{
    [Trace, JsonObject(MemberSerialization.Fields)]
    public class TechDemoScene : Scene
    {
        public ArtName CurrentBackground;
        public bool TrezorOpen;
        
        public List<Interactible> Items { get; } = new List<Interactible>();
        public Interactible Trezor { get; private set; } = null!;
        public Interactible Door { get; private set; } = null!;

        public override void Begin(Session hardSession)
        {
            base.Begin(hardSession);
            
            CurrentBackground = ArtName.InteriorGood;
            Interactible door = new Interactible(new Rectangle(171, 262, 153, 554), BookmarkId.Door, G.T("Zamčené. Nad klikou je nápis \"Anežka, Bětka, Eliška\"."))   {
                OnItemUse = new ScriptCode(new Script(BookmarkId.None, new QEvent[]
                {   
                    new QSfx(SoundEffectName.SfxDoorHandle),
                    new QSpeak("", "Cvak! Dveře se otevřely!", ArtName.Null, SpeakerPosition.Left),
                    new QSpeak("Skok", "Dobrá práce, Tišíku.", ArtName.SkokMluvici, SpeakerPosition.Left),
                    new QSpeak("Tišík", "Díky... šel bys dál jako první?", ArtName.TisikSpeaking, SpeakerPosition.Left),
                    new QSpeak("Skok", "Jasně, teď zjistíme, co se stalo s Akelou!", ArtName.SkokMluvici, SpeakerPosition.Left),
                    new QEnqueue(BookmarkId.R1_Victory)
                }))
            };
            Door = door;
            Items.Add(door);
            Items.Add(new Interactible(new Rectangle(907, 374, 134, 320), BookmarkId.Panenka, G.T("Hrozivě vypadající panenka."))
            {
                OnItemUse =new ScriptCode(new Script(BookmarkId.None, new QEvent[] {
                    new QSpeak("", "Tahle panenka není otevírací. Ten klíč nejspíš pasuje k něčemu jinému.", ArtName.Null, SpeakerPosition.Left)
                }))
            });
            Items.Add(new Interactible(new Rectangle(519, 234, 348, 219), BookmarkId.Window, G.T("Venku se žene příšerná blesková bouře.")));
            Items.Add(new Interactible(new Rectangle(1073, 218, 357, 223), BookmarkId.Picture, BookmarkId.Picture2));
            Trezor = new Interactible(new Rectangle(1474, 509, 412, 416), BookmarkId.Trezor, BookmarkId.Trezor2);
            Items.Add(Trezor);
        }

        public override void Draw(AirSession airSession)
        {
            Primitives.DrawZoomed(Library.Art(CurrentBackground), CurrentZoom);
            Primitives.DrawZoomed(TrezorOpen ? Library.Art(ArtName.InteriorTrezorOpen) : Library.Art(ArtName.InteriorTrezorClosed), CurrentZoom);
        }

        public override bool Click(AirSession airSession)
        {
            foreach (Interactible interactible in Items)
            {
                if (Root.IsMouseOver(interactible.Rectangle))
                {
                    var fullResolution = CommonGame.R1920x1080;
                    if (airSession.HeldItem == null)
                    {
                        airSession.Enqueue(new QZoomInto(interactible.Rectangle.Extend(100, 100), 0.1f));
                        airSession.Enqueue(new QWait(0.1f));
                        if (interactible.Interacted)
                        {
                            if (interactible.SecondEncounter != null)
                            {
                                airSession.Enqueue(new QSpeak("", interactible.SecondEncounter, ArtName.Null, SpeakerPosition.Left));
                            }
                            else
                            {
                                airSession.Enqueue(interactible.SecondEncounterAsStory);
                            }
                        }
                        else
                        {
                            airSession.Enqueue(interactible.FirstEncounter);
                            interactible.Interacted = true;
                        }

                        airSession.Enqueue(new QZoomInto(fullResolution, 0.1f));
                        airSession.Enqueue(new QWait(0.1f));
                    }
                    else
                    {
                        if (interactible.OnItemUse != null)
                        {
                            airSession.Enqueue(new QZoomInto(interactible.Rectangle.Extend(100, 100), 0.1f));
                            airSession.Enqueue(new QWait(0.1f));
                            CodeInput codeInput = new CodeInput()
                            {
                                InventoryItem = airSession.HeldItem,
                                AirSession = airSession
                            };
                            interactible.OnItemUse.Execute(codeInput);
                            airSession.Enqueue(new QZoomInto(fullResolution, 0.1f));
                            airSession.Enqueue(new QWait(0.1f));
                        }
                        else
                        {
                            airSession.Enqueue(new QSpeak("", "S touhle věcí klíč nesouvisí.", ArtName.Null, SpeakerPosition.Left));
                        }
                    }

                    return true;
                    
                }
            }

            return false;
        }
    }

    public class QEnqueue : QEvent
    {
        public BookmarkId TargetBookmark { get; }

        public QEnqueue(BookmarkId targetBookmark)
        {
            TargetBookmark = targetBookmark;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.Enqueue(Stories.Scripts.All[TargetBookmark]);
        }
    }
}