using System;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.SerializableCode;
using PostSharp.Community.ToString;

namespace Nsnbc.Stories
{
    [Trace]
    [JsonObject(MemberSerialization.Fields)]
    public class Interactible
    {
        public bool Interacted { get; [UsedImplicitly] set; }
        public Rectangle Rectangle { get; [UsedImplicitly] set; }
        public ArtName VisualArt { get; set; } = ArtName.Null;

        [IgnoreDuringToString] public InteractibleEncounter FirstEncounter { get; set; }
        [IgnoreDuringToString] public InteractibleEncounter? SecondEncounter { get; set; }

        public Code? OnItemUse { get; set; }

        public Interactible() // for deserializer
        {
        }

        public Interactible(Rectangle rectangle, InteractibleEncounter firstEncounter, InteractibleEncounter secondEncounter)
        {
            Rectangle = rectangle;
            FirstEncounter = firstEncounter;
            SecondEncounter = secondEncounter;
        }
    }
}