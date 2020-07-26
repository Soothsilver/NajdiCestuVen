using System;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.PostSharp;
using Nsnbc.SerializableCode;
using Nsnbc.Texts;

namespace Nsnbc.Stories
{
    [Trace]
    [JsonObject(MemberSerialization.Fields)]
    public class Interactible
    {
        public bool Interacted { get; [UsedImplicitly] set; }
        public Rectangle Rectangle { get; [UsedImplicitly] set; }
        public BookmarkId FirstEncounter { get; [UsedImplicitly] private set; }
        public GString? SecondEncounter { get; [UsedImplicitly] set; }
        public BookmarkId SecondEncounterAsStory { get; [UsedImplicitly] private set; }
        public Code? OnItemUse { get; set; }

        private Interactible() // for deserializer
        {
        }
        public Interactible(Rectangle rectangle, BookmarkId firstEncounter, GString secondEncounter)
        {
            Rectangle = rectangle;
            FirstEncounter = firstEncounter;
            SecondEncounter = secondEncounter;
        }
        public Interactible(Rectangle rectangle, BookmarkId firstEncounter, BookmarkId secondEncounter)
        {
            Rectangle = rectangle;
            FirstEncounter = firstEncounter;
            SecondEncounterAsStory = secondEncounter;
        }
    }
}