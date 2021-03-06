﻿using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Stories;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    internal class QSetInteractibleFirstAndSecondUse : QEvent
    {
        public string InteractibleName { get; }
        public Script Script { get; }

        public QSetInteractibleFirstAndSecondUse(string interactibleName, Script script)
        {
            InteractibleName = interactibleName;
            Script = script;
        }

        public override void Begin(AirSession airSession)
        {
            var interactible = airSession.Session.FindInteractible(InteractibleName)!;
            interactible.FirstEncounter = interactible.SecondEncounter = Script;
        }
    }
}