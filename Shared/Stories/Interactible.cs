using System;
using Microsoft.Xna.Framework;
using Nsnbc.Events;
using Nsnbc.PostSharp;

namespace Nsnbc.Stories
{
    [Trace]
    public class Interactible
    {
        public bool Interacted { get; set; }
        public Rectangle Rectangle { get; }
        public StoryId FirstEncounter { get; }
        public string? SecondEncounter { get; set; }
        public StoryId SecondEncounterAsStory { get; }
        public Action<InventoryItem, Session>? OnItemUse { get; set; }

        public Interactible(Rectangle rectangle, StoryId firstEncounter, string secondEncounter)
        {
            Rectangle = rectangle;
            FirstEncounter = firstEncounter;
            SecondEncounter = secondEncounter;
        }
        public Interactible(Rectangle rectangle, StoryId firstEncounter, StoryId secondEncounter)
        {
            Rectangle = rectangle;
            FirstEncounter = firstEncounter;
            SecondEncounterAsStory = secondEncounter;
        }
    }
}