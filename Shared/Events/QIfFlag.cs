﻿using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Stories;
using Nsnbc.Visiting;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)] 

    internal class QIfFlag : QEvent
    {
        public string FlagName { get; }
        public Script Then { get; }
        public Script ElseScript { get; }

        public QIfFlag(string flagName, Script then, Script elseScript)
        {
            FlagName = flagName;
            Then = then;
            ElseScript = elseScript;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.QuickEnqueue(airSession.Session.Flags.Contains(FlagName) ? Then : ElseScript);
        }

        public override void Accept(Visitor visitor)
        {
            base.Accept(visitor);
            Then.Accept(visitor);
            ElseScript.Accept(visitor);
        }
    }
}