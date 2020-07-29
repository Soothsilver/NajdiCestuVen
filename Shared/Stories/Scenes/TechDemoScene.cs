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

        public override IEnumerable<Interactible> Interactibles => Items;
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
            if (base.Click(airSession))
            {
                return true;
            }

            return false;
        }
    }
}