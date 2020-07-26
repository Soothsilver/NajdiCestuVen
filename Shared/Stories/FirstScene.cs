using System.Collections.Generic;
using Auxiliary;
using Microsoft.Xna.Framework;
using Nsnbc.Events;
using Nsnbc.PostSharp;

namespace Nsnbc.Stories
{
    [Trace]
    public class FirstScene
    {
        public bool TrezorOpen;
        public bool HideObjects = false;
        public List<Interactible> Items = new List<Interactible>();
        public Interactible Trezor { get; private set; } = null!;
        public Interactible Door { get; private set; } = null!;

        public void Begin(Session session)
        {
            Interactible door = new Interactible(new Rectangle(171, 262, 153, 554), StoryId.Door, G.T("Zamčené. Nad klikou je nápis \"Anežka, Bětka, Eliška\"."))   {
                OnItemUse = (it, ses) =>
                {   
                    ses.Enqueue(new QSfx(Sfxs.SfxDoorHandle));
                    ses.Enqueue(new QSpeak("", "Cvak! Dveře se otevřely!", ArtName.Null, SpeakerPosition.Left));
                    ses.Enqueue(new QSpeak("Skok", "Dobrá práce, Tišíku.", ArtName.SkokMluvici, SpeakerPosition.Left));
                    ses.Enqueue(new QSpeak("Tišík", "Díky... šel bys dál jako první?", ArtName.TisikSpeaking, SpeakerPosition.Left));
                    ses.Enqueue(new QSpeak("Skok", "Jasně, teď zjistíme, co se stalo s Akelou!", ArtName.SkokMluvici, SpeakerPosition.Left));
                    ses.Enqueue(StoryId.Victory);
                }
            };
            Door = door;
            Items.Add(door);
            Items.Add(new Interactible(new Rectangle(907, 374, 134, 320), StoryId.Panenka, G.T("Hrozivě vypadající panenka."))
            {
                OnItemUse = (it, ses) =>
                {
                    ses.Enqueue(new QSpeak("", "Tahle panenka není otevírací. Ten klíč nejspíš pasuje k něčemu jinému.", ArtName.Null, SpeakerPosition.Left));
                }
            });
            Items.Add(new Interactible(new Rectangle(519, 234, 348, 219), StoryId.Window, G.T("Venku se žene příšerná blesková bouře.")));
            Items.Add(new Interactible(new Rectangle(1073, 218, 357, 223), StoryId.Picture, StoryId.Picture2));
            Trezor = new Interactible(new Rectangle(1474, 509, 412, 416), StoryId.Trezor, StoryId.Trezor2);
            Items.Add(Trezor);
        }

        [Trace(AttributeExclude = true)]
        public void DrawBackground(Session session)
        {
            if (!HideObjects)
            {
                Root.SpriteBatch.Draw(
                    TrezorOpen ? Library.Art(ArtName.InteriorTrezorOpen) : Library.Art(ArtName.InteriorTrezorClosed),
                    session.FullResolution, session.CurrentZoom, Color.White);
            }
        }

        public bool Click(Session session)
        {
            foreach (Interactible interactible in Items)
            {
                if (Root.IsMouseOver(interactible.Rectangle))
                {
                    if (session.HeldItem == null)
                    {
                        session.Enqueue(new QZoomInto(interactible.Rectangle.Extend(100, 100), 0.1f));
                        session.Enqueue(new QWait(0.1f));
                        if (interactible.Interacted)
                        {
                            if (interactible.SecondEncounter != null)
                            {
                                session.Enqueue(new QSpeak("", interactible.SecondEncounter, ArtName.Null, SpeakerPosition.Left));
                            }
                            else
                            {
                                session.Enqueue(interactible.SecondEncounterAsStory);
                            }
                        }
                        else
                        {
                            session.Enqueue(interactible.FirstEncounter);
                            interactible.Interacted = true;
                        }

                        session.Enqueue(new QZoomInto(session.FullResolution, 0.1f));
                        session.Enqueue(new QWait(0.1f));
                    }
                    else
                    {
                        if (interactible.OnItemUse != null)
                        {
                            session.Enqueue(new QZoomInto(interactible.Rectangle.Extend(100, 100), 0.1f));
                            session.Enqueue(new QWait(0.1f));
                            interactible.OnItemUse(session.HeldItem, session);
                            session.Enqueue(new QZoomInto(session.FullResolution, 0.1f));
                            session.Enqueue(new QWait(0.1f));
                        }
                        else
                        {
                            session.Enqueue(new QSpeak("", "S touhle věcí klíč nesouvisí.", ArtName.Null, SpeakerPosition.Left));
                        }
                    }

                    return true;
                    
                }
            }

            return false;
        }
    }
}